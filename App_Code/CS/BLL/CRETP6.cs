using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.ServiceModel.Security;
using System.Xml;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

/// <summary>
/// Summary description for CRETP6
/// </summary>
public class CRETP6
{
    public CRETP6()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    CookieContainer cookieContainer;
    public void Login(String userName, String password)
    {

        AuthenticationService authService = new AuthenticationService();
        authService CookieContainer = new CookieContainer();

        authService.Url = ConfigurationManager.AppSettings["WSAuthenticationService"];
        AuthenticationServiceWebRef.Login loginObj = new AuthenticationServiceWebRef.Login();
        loginObj.UserName = userName;
        loginObj.Password = password;
        loginObj.DatabaseInstanceId = 1;
        loginObj.DatabaseInstanceIdSpecified = true;
        AuthenticationServiceWebRef.LoginResponse loginReturn = authService.Login(loginObj);
        cookieContainer = authService.CookieContainer;
    }

    public void Login(String userName, String password, String hostName, String port)
    {
        AuthenticationService authService = new AuthenticationService();
        authService.CookieContainer = new System.Net.CookieContainer();
        authService.Url = "http://" + hostName + ":" + port + P6WS_SERVICES_AUTHENTICATION_SERVICE;
        Login loginObj = new Login();
        loginObj.UserName = userName;
        loginObj.Password = password;
        loginObj.DatabaseInstanceId = 1;
        loginObj.DatabaseInstanceIdSpecified = true;
        loginObj.VerboseFaults = true;
        loginObj.VerboseFaultsSpecified = true;
        LoginResponse loginReturn = authService.Login(loginObj);
        cookieContainer = authService.CookieContainer;
    }
    public void Logout(String host, String port)
    {
        AuthenticationService authService = new AuthenticationService();
        authService.CookieContainer = cookieContainer;
        authService.Url = "http://" + host + ":" + port + "/p6ws/services/AuthenticationService";
        LogoutResponse logoutReturn = authService.Logout(""); ;
    }
    public int ReadProject(String hostName, String portNumber, String projectId)
    {
        ProjectPortBinding apb = new ProjectPortBinding();
        apb.CookieContainer = cookieContainer;
        apb.Url = HTTP + hostName + ":" + portNumber + P6WS_SERVICES_PROJECT_SERVICE;
        ReadProjects rp = new ReadProjects();
        Primavera.Ws.P6.Project.ProjectFieldType[] projectFields = new Primavera.Ws.P6.Project.ProjectFieldType[1];
        projectFields[0] = Primavera.Ws.P6.Project.ProjectFieldType.ObjectId;
        rp.Filter = "Id='" + projectId + "'";
        rp.Field = projectFields;
        Primavera.Ws.P6.Project.Project[] projects = apb.ReadProjects(rp);
        if (projects != null && projects.Length > 0)
        {
            return (int)projects[0].ObjectId;
        }
        return 0;
    }
    public int[] CreateActivities(String hostName, String port, int projectObjectId)
    {
        ActivityPortBinding apb = new ActivityPortBinding();
        apb.CookieContainer = cookieContainer;
        apb.Url = HTTP + hostName + ":" + port + P6WS_SERVICES_ACTIVITY_SERVICE;
        Primavera.Ws.P6.Activity.Activity[] acts;
        acts = new Primavera.Ws.P6.Activity.Activity[3];
        Primavera.Ws.P6.Activity.Activity activity = null;
        for (int i = 0; i < 3; i++)
        {
            activity = new Primavera.Ws.P6.Activity.Activity();
            activity.ProjectObjectId = projectObjectId;
            activity.ProjectObjectIdSpecified = true;
            activity.Id = "TestAct" + (i + 1);
            activity.Name = "TestAct" + (i + 1);
            acts[i] = activity;
        }
        return apb.CreateActivities(acts);
    }


    public class CustomTokenSerializer : WSSecurityTokenSerializer
    {
        public CustomTokenSerializer(SecurityVersion sv)
            : base(sv) { }

        protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
        {
            var userToken = token as UserNameSecurityToken;
            var tokennamespace = "o";
            var nonce = GetSHA1String(Guid.NewGuid().ToString());
            writer.WriteRaw(
                    $@"<{tokennamespace}:UsernameToken u:Id=""{token.Id}"" xmlns:u=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"">
                <{tokennamespace}:Username>{userToken.UserName}</{tokennamespace}:Username>
                <{tokennamespace}:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText\"">{userToken.Password}</{tokennamespace}:Password>
                <{tokennamespace}:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\"">{nonce}</{tokennamespace}:Nonce>
            </{tokennamespace}:UsernameToken>"
                );
        }

        protected string GetSHA1String(string phrase)
        {
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(phrase));
            return Convert.ToBase64String(hashedDataBytes);
        }
    }

    public class WebServiceClientFactory<T> : WebServiceClientFactory<T>
    {
        public WebServiceClientFactory()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
        }

        public T GetClient(Credentials cred)
        {

            ChannelFactory<T> channelFactory = new ChannelFactory<T>(GetBinding(), new EndpointAddress(cred.Url));
            channelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
            channelFactory.Endpoint.Behaviors.Add(new CustomCredentials());
            channelFactory.Endpoint.Behaviors.Add(new CustomP6DbInstanceBehavior(cred.DatabaseInstanceId));

            channelFactory.Credentials.UserName.UserName = cred.Username;
            channelFactory.Credentials.UserName.Password = cred.Password;

            return channelFactory.CreateChannel();
        }

        private Binding GetBinding()
        {
            var security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            security.IncludeTimestamp = false;
            var encoding = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            var transport = new HttpsTransportBindingElement
            {
                MaxReceivedMessageSize = 20000000 // 20 megs
            };
            return new CustomBinding(security, encoding, transport);
        }

    }
}
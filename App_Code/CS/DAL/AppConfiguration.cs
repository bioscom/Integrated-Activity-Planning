using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

public static class AppConfiguration
{
    // Caches the connection string
    public static string dbConnectionString;
    // Caches the data provider name
    public static string dbProviderName;
    // Store the number of products per page
    private readonly static int productsPerPage;
    // Store the product description length for product lists
    private readonly static int productDescriptionLength;
    // Store the name of your shop
    private readonly static string siteName;
    private readonly static string siteShortName;
    //private readonly static string lifeServerHost;
    //private readonly static string testServerHost;
    public static string siteHostName;
    public static string DevelopmentEnvironment;
    public static string AppNameId { get; set; }
    public static string adminName { get; set; }
    public static string adminEmail { get; set; }
    public static string adminUserName { get; set; }
    public static string adminExt { get; set; }

    public static string smtpHost { get; set; }
    public static bool bccAdmin { get; set; }

    //---------- Menu ---------------//
    public static string AdminMenu { get; set; }
    public static string ActivityOwnerMenu { get; set; }
    public static string ApproversMenu { get; set; }
    public static string PlannersMenu { get; set; }

    public static string FunctionalAccount { get; set; }


    static AppConfiguration()
    {
        dbConnectionString = ConfigurationManager.ConnectionStrings["IAPConnectionString"].ConnectionString;
        dbProviderName = ConfigurationManager.ConnectionStrings["IAPConnectionString"].ProviderName;

        productsPerPage = System.Int32.Parse(ConfigurationManager.AppSettings["ProductsPerPage"]);
        productDescriptionLength = System.Int32.Parse(ConfigurationManager.AppSettings["ProductDescriptionLength"]);
        siteName = ConfigurationManager.AppSettings["SiteName"];
        AppNameId = ConfigurationManager.AppSettings["AppNameId"];
        siteShortName = ConfigurationManager.AppSettings["SiteShortName"];
        //lifeServerHost = ConfigurationManager.AppSettings["LifeServerHost"];
        siteHostName = ConfigurationManager.AppSettings["SiteHostName"];
        //testServerHost = ConfigurationManager.AppSettings["TestServerHost"];
        DevelopmentEnvironment = ConfigurationManager.AppSettings["DevelopmentEnvironment"];

        smtpHost = ConfigurationManager.AppSettings["MailServer"];
        adminName = ConfigurationManager.AppSettings["iap.admin.name"];
        adminEmail = ConfigurationManager.AppSettings["iap.admin.mail"];
        adminUserName = ConfigurationManager.AppSettings["cpdms.AdminUserName"];
        adminExt = ConfigurationManager.AppSettings["iap.admin.ext"];
        bccAdmin = bool.Parse(ConfigurationManager.AppSettings["cpdms.BccAdmin"]);

        AdminMenu = ConfigurationManager.AppSettings["AdminMenu"];
        ActivityOwnerMenu = ConfigurationManager.AppSettings["ActivityOwnerMenu"];
        ApproversMenu = ConfigurationManager.AppSettings["ApproversMenu"];
        PlannersMenu = ConfigurationManager.AppSettings["PlannersMenu"];

        FunctionalAccount = ConfigurationManager.AppSettings["FunctionalAccount"];
    }
    // Returns the connection string for the CPDMS database
    public static string DbConnectionString
    {
        get
        {
            return dbConnectionString;
        }
    }
    // Returns the data provider name
    public static string DbProviderName
    {
        get
        {
            return dbProviderName;
        }
    }
    // Returns the address of the mail server
    public static string MailServer
    {
        get
        {
            return ConfigurationManager.AppSettings["MailServer"];
        }
    }
    // Returns the email username
    public static string MailUsername
    {
        get
        {
            return ConfigurationManager.AppSettings["MailUsername"];
        }
    }
    // Returns the email password
    public static string MailPassword
    {
        get
        {
            return ConfigurationManager.AppSettings["MailPassword"];
        }
    }
    // Returns the email password
    public static string MailFrom
    {
        get
        {
            return ConfigurationManager.AppSettings["MailFrom"];
        }
    }
    // Send error log emails?
    public static bool EnableErrorLogEmail
    {
        get
        {
            return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
        }
    }
    // Returns the email address where to send error reports
    public static string ErrorLogEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["ErrorLogEmail"];
        }
    }

    // Returns the maximum number of products to be displayed on a page
    public static int ProductsPerPage
    {
        get
        {
            return productsPerPage;
        }
    }
    // Returns the length of product descriptions in products lists
    public static int ProductDescriptionLength
    {
        get
        {
            return productDescriptionLength;
        }
    }
    // Returns the length of product descriptions in products lists
    public static string SiteName
    {
        get
        {
            return siteName;
        }
    }

    public static string SiteHostName
    {
        get
        {
            return siteHostName;
        }
    }

    public static string SiteDevelopmentEnvironment
    {
        get
        {
            return DevelopmentEnvironment;
        }
    }


    public static string SiteShortName
    {
        get { return AppConfiguration.siteShortName; }
    }

    //public static string LifeServerHost
    //{
    //    get { return AppConfiguration.lifeServerHost; }
    //}

    //public static string TestServerHost
    //{
    //    get { return AppConfiguration.testServerHost; }
    //} 
}
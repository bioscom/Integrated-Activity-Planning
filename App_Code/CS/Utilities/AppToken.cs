using System;

	/// <summary>
	/// Summary description for AppToken.
	/// </summary>
public class AppToken
{
    private string mLoginID;
    private string mAppName;
    private int mLoginKey;
    private int mAppKey;
    //private string mRole;

		public AppToken()
		{
			mLoginID = String.Empty;
            mAppName = String.Empty;
            //mAppRole = String.Empty;
		}

    public string LoginID
    {
      get { return mLoginID; }
      set { mLoginID = value; }
    }

    public string AppName
    {
      get { return mAppName; }
      set { mAppName = value; }
    }

    //public string AppRole
    //{
    //    get { return mAppRole; }
    //    set { mAppRole = value; }
    //}

    public int LoginKey
    {
      get { return mLoginKey; }
      set { mLoginKey = value; }
    }

    public int AppKey
    {
      get { return mAppKey; }
      set { mAppKey = value; }
    }
}

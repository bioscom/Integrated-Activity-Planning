/// <summary>
/// Summary description for Apps.
/// </summary>
public static class Apps
{
	static Apps()
	{
        
	}


    // 3.   The Method that Generates Token

    public static string GenerateToken()
    {
      return System.Guid.NewGuid().ToString();
    }
    
    public static string LoginIDNoDomain(string loginID)
    {
      if (loginID.IndexOf("\\") >= 0)
      {
          loginID = loginID.Substring(loginID.IndexOf("\\") + 1);
      }

      return loginID;
    }
}
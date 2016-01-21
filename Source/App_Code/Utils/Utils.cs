using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.DirectoryServices;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
	public Utils()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string ViewStringValue(string str)
    {
        if (str == null)
        {
            return "NULL";
        }
        else
        {
            return str;
        }
    }

    public static string LeftNChar(string str, int n)
    {
        string ret = null;
        if (str != null)
        {
            if (n >= str.Length)
            {
                ret = str;
            }
            else
            {
                ret = str.Substring(0, n);
            }
        }
        return ret;
    }

    public static bool IsNullOrEmpty(string s)
    {
        if (s != null)
        {
            return (s.Length <= 0);
        }
        else
        {
            return true;
        }
    }

    public static string AppendEndingPathSlash(string path)
    {
        if (!Utils.IsNullOrEmpty(path))
        {
            if (!path.EndsWith(@"\")) path = path + @"\";
        }
        return path;
    }

    public static string RemoveBeginningSinglePathSlash(string path)
    {
        if (!Utils.IsNullOrEmpty(path))
        {
            if (path.StartsWith(@"\"))
            {
                if (!path.StartsWith(@"\\"))
                {
                    path = path.Substring(1);
                }
            }
        }
        return path;
    }

    public static bool AuthenticateLDAP(string username, string password, string domain)
    {
        //return true;
        // authenticate using ActiveDirectory
        using (DirectoryEntry deDirEntry = new DirectoryEntry("LDAP://" + domain,
            username, password, AuthenticationTypes.Secure))
        {
            try
            {
                string tmpCheck = deDirEntry.Name; // check if login is successful
            }
            catch (Exception)
            {
                return false;
            }
        }
        return true;
    }

    public static byte[] LoadFile(string fileFullPathName)
    {
        byte[] fileData;
        FileStream fs = File.Open(fileFullPathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        try
        {
            if (fs.Length >= int.MaxValue) throw new InvalidDataException("Big file size is not supported. File '" + fileFullPathName + "' is too large to load");
            int fileLength = (int)fs.Length;
            fileData = new byte[fileLength];
            fs.Read(fileData, 0, fileLength); // big file (size > 2^32 bytes) is not supported
        }
        finally
        {
            fs.Close();
        }
        return fileData;
    }

    public static DateTime ConvertToDBDateTime(String value, String format)
    {
        DateTime date;
        if (DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out date))
        {
             return date;
        }
        return DateTime.MaxValue;
    }
}

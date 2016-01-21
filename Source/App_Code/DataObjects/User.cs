using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;
/// <summary>
/// Summary description for User
/// </summary>
namespace BrokerManager.DataObjects
{
    public class UserDTO
    {
        #region Static Data
        public static Hashtable HashAllUser = new Hashtable();
        #endregion

        #region Private Members
        private string _id = string.Empty;
        private string _password = string.Empty;
        private List<Privilege> _listPrivilege;
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
            set { if (value != null) _id = value; }
        }
        public string Password
        {
            get { return _password; }
            set { if (value != null) _password = value; }
        }
        public List<Privilege> ListPrivilege
        {
            get { return _listPrivilege; }
            set { _listPrivilege = value; }
        }
        #endregion

        #region Constructors
        public UserDTO()
	    {
            _listPrivilege = new List<Privilege>();
	    }

        public UserDTO(string username, string password) : this()
        {
            _id = username;
            _password = password;
        }
        #endregion

        #region Static Functions
        public static void GetGlobalUserData()
        {
            UserDAL dalUser = new UserDAL();
            Hashtable hashUser = dalUser.GetAllUserInformation();
            UserDTO.HashAllUser = hashUser;
        }

        public static UserDTO Authenticate(UserDTO user, out string errorMessage)
        {
            
            errorMessage = null;
            UserDTO systemUser = null;
            if (user != null)
            {
                if (user.Id != null)
                {
                    systemUser = (UserDTO)UserDTO.HashAllUser[user.Id];
                    if (systemUser != null)
                    {
                        if (systemUser.Password.Equals(user.Password))
                        {
                            return systemUser;
                        }
                        else
                        {
                            string authenticationDomain = ConfigurationManager.AppSettings[Constants.CONF_ID_AUTHENTICATION_DOMAIN];
                            if (Utils.AuthenticateLDAP(user.Id, user.Password, authenticationDomain))
                            {
                                return systemUser;
                            }
                            else
                            {
                                errorMessage = "Incorrect username/password or domain authentication fails";
                                return null;
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "User not found";
                    }
                }
            }

            return systemUser;
        }
        #endregion

        #region Public Functions
        #endregion
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Privilege
/// </summary>
namespace BrokerManager.DataObjects
{
    public class Privilege
    {
        private string _id;
        private string _name;
        private string _description;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Privilege()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

}
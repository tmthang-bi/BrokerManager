using System;
using System.Data;
using System.Configuration;


/// <summary>
/// Summary description for Fee
/// </summary>
namespace BrokerManager.DataObjects
{
    public class Fee
    {
        public const string FEE_OPT = "1";
        public const string FEE_VIS = "2";

        private string _id;
        private string _name;
        private long _defaultAmount;
        private double _defaultRate;
        private string _description;
        private string _createBy;
        private DateTime _createTime;
        private string _updateBy;
        private DateTime _updateTime;

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
        public long DefaultAmount
        {
            get { return _defaultAmount; }
            set { _defaultAmount = value; }
        }
        public double DefaultRate
        {
            get { return _defaultRate; }
            set { _defaultRate = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string CreateBy
        {
            get { return _createBy; }
            set { _createBy = value; }
        }
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public string UpdateBy
        {
            get { return _updateBy; }
            set { _updateBy = value; }
        }
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        public Fee()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
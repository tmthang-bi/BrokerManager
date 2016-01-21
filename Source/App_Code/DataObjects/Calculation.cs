using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for BrokerType
/// </summary>
namespace BrokerManager.DataObjects
{
    public class CalculationDTO
    {
       
       
        private DateTime calculPeriod;
        private string id;
        private string calculatedBy;
        private bool freeze = false;
        private bool verifyTradingData = false;
        
        public DateTime CalculPeriod
        {
            get { return calculPeriod; }
            set { calculPeriod = value; }
        }
                
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
                
        public string CalculatedBy
        {
            get { return calculatedBy; }
            set { calculatedBy = value; }
        }

        public bool Freeze 
        {
            get { return freeze; }
            set { freeze = value; }
        }
        public bool VerifyTradingData
        {
            get { return verifyTradingData; }
            set { verifyTradingData = value; }
        }

        #region Properties

        #endregion

        #region Constructor
        public CalculationDTO()
        {
        }
        #endregion

        #region Private Methods
        #endregion
                
    }
}
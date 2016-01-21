using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for Broker
/// </summary>
namespace BrokerManager.DataObjects
{
    public class BrokerRate
    {
        #region Global Static Data
        #region Constants
        #endregion
        #endregion

        #region Properties
        private Broker _broker;
        public Broker Broker
        {
            get { return _broker; }
            set { _broker = value; }
        }

        private double _adjustRate = 1.0;
        public double AdjustRate
        {
            get { return _adjustRate; }
            set { _adjustRate = value; }
        }
        #endregion

        #region Constructor
        public BrokerRate(Broker broker)
        {
            this.Broker = broker;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

    }
}
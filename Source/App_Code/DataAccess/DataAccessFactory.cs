using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for DataAccessFactory
/// </summary>
namespace BrokerManager.DataAccess
{
    public class DataAccessFactory
    {

        public static IDataAccess CreateDataAccess()
        {
            return new SQLServerDataAccess();
        }
    }
}

using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for IDataAccess
/// </summary>
namespace BrokerManager.DataAccess
{
    public interface IDataAccess
    {
        String ConnectionString { get; set; }
        IDbCommand Command { get; }

        DataSet ExecuteQuery();
        DataSet ExecuteQuery(string query);
        DataSet ExecuteQuery(string query, CommandType commandType);
        int ExecuteNonQuery();
        int ExecuteNonQuery(string sql);
        int ExecuteNonQuery(string sql, CommandType commandType);
    }
}

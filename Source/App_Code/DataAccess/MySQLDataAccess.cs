using System;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BrokerManager.DataAccess
{
    /// <summary>
    /// Summary description for MySQLDataAccess
    /// </summary>
    public class MySQLDataAccess : IDataAccess
    {
        #region Members
        private String _connectionString = string.Empty;
        private MySqlConnection _connection = null;
        private MySqlCommand _command = null;
        #endregion

        #region Properties
        public String ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (value != null)
                {
                    if (!value.Equals(_connectionString))
                    {
                        // Close old connection (or return it to the connection pool managed by the framework)
                        CloseConnection();

                        // Release resources associate with old connection
                        if (_command != null) _command.Dispose();
                        if (_connection != null) _connection.Dispose();

                        // Create new connection (or take it from the framework's connection pool) & command
                        _connectionString = value;
                        _connection = new MySqlConnection(_connectionString);
                        _command = new MySqlCommand();
                        _command.Connection = _connection;
                    }
                }
            }
        }
        public IDbCommand Command
        {
            get { return _command; }
            //set { _command = value; }
        }
        #endregion

        #region Constructors
        public MySQLDataAccess()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[Constants.CONF_ID_CONNECTIONSTRING_MYSQL].ConnectionString;
            if (connectionString != null)
            {
                ConnectionString = connectionString;
            }
            else
            {
                throw new InvalidOperationException("Invalid connection string");
            }
        }

        public MySQLDataAccess(string connectionString)
        {
            if (connectionString != null)
            {
                ConnectionString = connectionString;
            }
            else
            {
                throw new InvalidOperationException("Invalid connection string");
            }
        }
        #endregion

        #region Private Functions
        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        #endregion

        #region Public Functions
        public DataSet ExecuteQuery()
        {
            DataSet retData = null;
            if ((_command.CommandText == null) || (_command.CommandText.Trim().Length <= 0)) throw new InvalidOperationException("SQL command text not set");
            OpenConnection();
            retData = new DataSet();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(_command);
            try
            {
                dataAdapter.Fill(retData);
            }
            finally
            {
                dataAdapter.Dispose();
                CloseConnection();
            }
            return retData;
        }

        public DataSet ExecuteQuery(string query)
        {
            _command.CommandText = query;
            _command.CommandType = CommandType.Text;
            return ExecuteQuery();
        }

        public DataSet ExecuteQuery(string query, CommandType commandType)
        {
            _command.CommandText = query;
            _command.CommandType = commandType;
            return ExecuteQuery();
        }

        public int ExecuteNonQuery()
        {
            int ret = 0;
            if ((_command.CommandText == null) || (_command.CommandText.Trim().Length <= 0)) throw new InvalidOperationException("SQL command text not set");
            OpenConnection();
            try
            {
                ret = _command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
            return ret;
        }

        public int ExecuteNonQuery(string sql)
        {
            _command.CommandText = sql;
            _command.CommandType = CommandType.Text;
            return ExecuteNonQuery();
        }

        public int ExecuteNonQuery(string sql, CommandType commandType)
        {
            _command.CommandText = sql;
            _command.CommandType = commandType;
            return ExecuteNonQuery();
        }
        #endregion
    }
}
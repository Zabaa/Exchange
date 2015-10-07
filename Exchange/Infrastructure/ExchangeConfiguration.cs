using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Exchange.Infrastructure
{
    public static class ExchangeConfiguration
    {
        #region Constants Keys

        private const string ConnectionStringConfigKey = "Exchange";

        #endregion

        #region Private Fields

        private static string _connectionString;

        #endregion

        #region Accessors

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[ConnectionStringConfigKey].ConnectionString))
                        _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringConfigKey].ConnectionString;
                }
                return _connectionString;
            }
        }

        #endregion
    }
}
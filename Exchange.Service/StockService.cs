﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain;

namespace Exchange.Service
{
    public class StockService
    {
        public IEnumerable<Stock> GetStocks()
        {
            using (var connnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Exchange"].ConnectionString))
            {
                connnection.Open();
                using (var command = new SqlCommand("sp_GetStocks", connnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Notification = null;

                    if(connnection.State == ConnectionState.Closed)
                        connnection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Cast<IDataRecord>().Select(x => new Stock
                        {
                            Id = x.GetInt32(0),
                            Symbol = x.GetString(1),
                            Price = x.GetDecimal(2),
                            DayOpen = x.GetDecimal(3),
                            LastChangeDate = x.GetDateTime(4)
                        }).ToList();
                    }
                }
            }
        }
    }
}

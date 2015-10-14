using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain;
using Exchange.Abstract.Services;
using Exchange.DataAccess.Context;

namespace Exchange.Service
{
    public class StockService : IStockService
    {
        private readonly ExchangeContext _exchangeContext;

        public StockService()
        {
            _exchangeContext = new ExchangeContext();
        }

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

        public Stock GetStock(int id)
        {
            return _exchangeContext.Stocks.SingleOrDefault(s => s.Id == id);
        }

        public void UpdateStockPrice(int id, decimal newPrice)
        {
            var stock = _exchangeContext.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock != null)
            {
                stock.Price = newPrice;
                _exchangeContext.SaveChanges();
            }
        }
    }
}

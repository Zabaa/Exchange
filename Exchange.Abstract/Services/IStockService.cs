using Exchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Abstract.Services
{
    public interface IStockService
    {
        IEnumerable<Stock> GetStocks();

        Stock GetStock(int id);

        void UpdateStockPrice(int id, decimal newPrice);
    }
}

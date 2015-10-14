using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.DataAccess.Context;

namespace EFTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbContext = new ExchangeContext();
            var stocks = dbContext.Stocks.ToList();

            foreach (var stock in stocks)
            {
                Console.WriteLine("{0} - {1}", stock.Symbol, stock.Price);
            }

            Console.ReadLine();
        }
    }
}

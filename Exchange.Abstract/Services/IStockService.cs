using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Abstract.Services
{
    public delegate void ResultChangedEventHandler(object sender);

    public interface IStockService
    {
        event ResultChangedEventHandler StockChanged;
    }
}

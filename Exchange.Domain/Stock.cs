using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain
{
    public class Stock : BaseEntity
    {
        private decimal _price;

        public string Symbol { get; set; }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                //if (_price == value)
                //{
                //    return;
                //}

                //_price = value;
                //LastChangeDate = DateTime.Now;

                //if (DayOpen == 0)
                //{
                //    DayOpen = _price;
                //}

                _price = value;
            }
        }

        public decimal DayOpen { get; set; }

        public decimal Change
        {
            get
            {
                return Price - DayOpen;
            }
        }

        public double PercentChange
        {
            get
            {
                return (double)Math.Round(Change / Price, 4);
            }
        }

        public DateTime LastChangeDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.ViewModel
{
    public class BasePaginViewModel<T> where T : BaseGridViewModel
    {
        public int PageSize { get; set; }
        public int TotalElementCount { get; set; }
        public string OrderBy { get; set; }
        public bool IsDesc { get; set; }
        public string SearchPattern { get; set; }

        public IEnumerable<T> GridViewModels { get; set; }
    }
}

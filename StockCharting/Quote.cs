using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCharting {
    public class Quote {
        public DateTime Date { get; set; }
        public Double Close { get; set; }
    }

    public class QuoteList : List<Quote> {

    }
}

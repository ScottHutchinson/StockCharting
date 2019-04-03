using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace StockCharting {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        /* See https://eodhistoricaldata.com/knowledgebase/api-for-historical-data-and-volumes/#JSON_Output_Support */

        [STAThread]
        static void Main() {
            const string OloUrl = "https://eodhistoricaldata.com/api/eod/AAPL.US?from=2019-03-01&to=2019-04-02&api_token=OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX&period=d&fmt=json";
            var json = String.Empty;
            using (WebClient wc = new WebClient()) {
                json = wc.DownloadString(OloUrl);
            }
            JArray jarr = JArray.Parse(json);
            var quotes = jarr
                .Select(q => new Quote { Date = DateTime.Parse(q["date"].ToString()), Close = Double.Parse(q["close"].ToString()) })
                .Skip(Math.Max(0, jarr.Count() - 10)) // TakeLast 10 quotes.
                .ToList();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(quotes));
        }
    }
}

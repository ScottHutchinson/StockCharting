using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockCharting {
    public partial class Form1 : Form {
        private IEnumerable<Quote> _quotes;

        public Form1(IEnumerable<Quote> quotes) {
            _quotes = quotes;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            chart1.DataSource = _quotes;
            chart1.Series[0].Points.DataBindXY(_quotes, "Date", _quotes, "Close");
            chart1.ChartAreas[0].AxisY.Minimum = 180;
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                TextAnnotation textAnnotation = new TextAnnotation();
                textAnnotation.Text = "Hmmm...";
                textAnnotation.SetAnchor(chart1.Series[0].Points[4]);
                chart1.Annotations.Add(textAnnotation);
            }
        }
    }
}

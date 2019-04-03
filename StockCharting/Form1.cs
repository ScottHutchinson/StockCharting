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

        private int getNearestIdx(double x, List<double> xValues) {
            var idx = xValues.BinarySearch(x);
            if (Math.Abs(idx) > xValues.Count - 1) {
                return xValues.Count - 1;
            }
            else if (idx < 0) {
                var nextIdx = ~idx;
                if (nextIdx == 0) {
                    return 0;
                }
                else {
                    var lower = xValues[nextIdx - 1];
                    var higher = xValues[nextIdx];
                    var nearestIdx = Math.Abs(lower - x) < Math.Abs(higher - x) ? nextIdx - 1 : nextIdx;
                    return nearestIdx;
                }
            }
            else {
                return idx; // exact hit
            }
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                var x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                var xValues = chart1.Series[0].Points.Select(p => p.XValue).ToList();
                var nearestIdx = getNearestIdx(x, xValues);
                var textAnnotation = new TextAnnotation {
                    Text = "Hmmm..." // TODO: Prompt the user for the text.
                };
                textAnnotation.SetAnchor(chart1.Series[0].Points[nearestIdx]);
                chart1.Annotations.Add(textAnnotation);
            }
        }
    }
}

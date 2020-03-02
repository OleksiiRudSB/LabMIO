using LabMIO.Systems;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;

namespace LabMIO.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ISystem System { get; }

        private double time = 0;

        public MainWindowViewModel()
        {
            System = new TestSystem();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double>(),
                    PointGeometry = null
                }
            };

            Labels = null;
            YFormatter = value => value.ToString();


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(CalculateSystem);
            dispatcherTimer.Interval = new TimeSpan(3000);
            dispatcherTimer.Start();

        }

        private void CalculateSystem(object sender, EventArgs e)
        {
            var result = System.Next(1);
            //Labels.Append(time.ToString());
            SeriesCollection[0].Values.Add(result);
            time += 0.05;
        }
    }
}

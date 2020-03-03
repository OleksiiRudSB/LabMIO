using LabMIO.Commands;
using LabMIO.Systems;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace LabMIO.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Field
        private double _x1;
        private double _x2;
        private double _x1_2;
        private double _xout1;
        private double _z1;
        private double _z2;

        private ControlSystem ControlSystem;
        private double time = 0;
        private DispatcherTimer dispatcherTimer;
        #endregion

        #region Public Properties
        public SeriesCollection Z1SeriesCollection { get; set; }
        public SeriesCollection Z2SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


        public double x1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged(nameof(x1));
            }
        }

        public double x2
        {
            get => _x2;
            set
            {
                _x2 = value;
                OnPropertyChanged(nameof(x2));
            }
        }

        public double x1_2
        {
            get => _x1_2;
            set
            {
                _x1_2 = value;
                OnPropertyChanged(nameof(x1_2));
            }
        }

        public double xout1
        {
            get => _xout1;
            set
            {
                _xout1 = value;
                OnPropertyChanged(nameof(xout1));
            }
        }

        public double z1
        {
            get => _z1;
            set
            {
                _z1 = value;
                OnPropertyChanged(nameof(z1));
            }
        }

        public double z2
        {
            get => _z2;
            set
            {
                _z2 = value;
                OnPropertyChanged(nameof(z2));
            }
        }
        #endregion

        #region Commands
        public CalculateCommand IncreaseX1
        {
            get => new CalculateCommand((o) => ++x1);
        }
        public CalculateCommand IncreaseX2
        {
            get => new CalculateCommand((o) => ++x2);
        }
        public CalculateCommand IncreaseX1_2
        {
            get => new CalculateCommand((o) => ++x1_2);
        }
        public CalculateCommand IncreaseXout1
        {
            get => new CalculateCommand((o) => ++xout1);
        }

        public CalculateCommand DecreaseX1
        {
            get => new CalculateCommand((o) => --x1);
        }
        public CalculateCommand DecreaseX2
        {
            get => new CalculateCommand((o) => --x2);
        }
        public CalculateCommand DecreaseX1_2
        {
            get => new CalculateCommand((o) => --x1_2);
        }
        public CalculateCommand DecreaseXout1
        {
            get => new CalculateCommand((o) => --xout1);
        }

        public CalculateCommand StartTimer
        {
            get => new CalculateCommand((o) => dispatcherTimer.Start());
        }
        public CalculateCommand StopTimer
        {
            get => new CalculateCommand((o) => dispatcherTimer.Stop());
        }
        #endregion


        public MainWindowViewModel()
        {
            ControlSystem = new ControlSystem(1, 20, 25, 1);

            InitCharts();
            InitTimer();
        }

        private void InitCharts()
        {
            Z1SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Z1",
                    Values = new ChartValues<double>(),
                    PointGeometry = null
                }
            };

            Z2SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Z2",
                    Values = new ChartValues<double>(),
                    PointGeometry = null
                }
            };

            Labels = null;
            YFormatter = value => value.ToString();
        }

        private void InitTimer()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(CalculateSystem);
            dispatcherTimer.Interval = new TimeSpan(1000);
        }

        private void CalculateSystem(object sender, EventArgs e)
        {
            z1 = ControlSystem.CalculateZ1(x1,x2,x1_2,xout1);
            z2 = ControlSystem.CalculateZ2(x1, x2, x1_2);
            Z1SeriesCollection[0].Values.Add(z1);
            Z2SeriesCollection[0].Values.Add(z2);
            if(Z1SeriesCollection[0].Values.Count > 200)
            {
                Z1SeriesCollection[0].Values.RemoveAt(0);
            }
            if (Z2SeriesCollection[0].Values.Count > 200)
            {
                Z2SeriesCollection[0].Values.RemoveAt(0);
            }
            time += 0.05;
        }
    }
}

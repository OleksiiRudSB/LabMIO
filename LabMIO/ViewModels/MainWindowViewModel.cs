using LabMIO.Commands;
using LabMIO.Data.Blocks;
using LabMIO.Systems;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace LabMIO.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Field
        private bool _isX10;
        private Visibility _pidSettingsVisability;
        private double _x1;
        private double _x2;
        private double _x1_2;
        private double _xout1;
        private double _z1;
        private double _z2;
        private PIDBlock _pid;

        private ControlSystem _controlSystem;
        private double time = 0;
        private DispatcherTimer dispatcherTimer;
        #endregion

        #region Public Properties
        public SeriesCollection ZSeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


        public ControlSystem ControlSystem 
        {
            get => _controlSystem;
            set => SetProperty(ref _controlSystem, value);
        }

        public PIDBlock PID
        {
            get => _pid;
            set => SetProperty(ref _pid, value);
        }

        public Visibility PIDSettingsVisability
        {
            get => _pidSettingsVisability;
            set => SetProperty(ref _pidSettingsVisability, value);
        }

        public double x1
        {
            get => _x1;
            set => SetProperty<double>(ref _x1, value);
        }

        public double x2
        {
            get => _x2;
            set => SetProperty<double>(ref _x2, value);
        }

        public double x1_2
        {
            get => _x1_2;
            set => SetProperty<double>(ref _x1_2, value);
        }

        public double xout1
        {
            get => _xout1;
            set => SetProperty<double>(ref _xout1, value);
        }

        public double z1
        {
            get => _z1;
            set => SetProperty<double>(ref _z1, value);
        }

        public double z2
        {
            get => _z2;
            set => SetProperty<double>(ref _z2, value);
        }
        #endregion

        #region Commands
        public ICommand ChangeMode { get; set; }
        public ICommand x10 { get; set; }
        public ICommand IncreaseX1 { get; set; }
        public ICommand IncreaseX2 { get; set; }
        public ICommand IncreaseX1_2 { get; set; }
        public ICommand IncreaseXout1 { get; set; }
        public ICommand DecreaseX1 { get; set; }
        public ICommand DecreaseX2 { get; set; }
        public ICommand DecreaseX1_2 { get; set; }
        public ICommand DecreaseXout1 { get; set; }
        public ICommand StartTimer { get; set; }
        public ICommand StopTimer { get; set; }
        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            var dt = 1;
            PID = new PIDBlock(1, 1, 1, dt);
            ControlSystem = new ControlSystem(PID, dt);

            InitCommands();
            InitCharts();
            InitTimer();
            SetVisability();
        }
        #endregion

        #region Privete Methods
        private void InitCommands()
        {
            ChangeMode = new Command(() => ChangeSystemMode());
            x10 = new Command(() => SetTimer());
            IncreaseX1 = new Command(() => ++x1);
            IncreaseX2 = new Command(() => ++x2);
            IncreaseX1_2 = new Command(() => ++x1_2);
            IncreaseXout1 = new Command(() => ++xout1);
            DecreaseX1 = new Command(() => --x1);
            DecreaseX2 = new Command(() => --x2);
            DecreaseX1_2 = new Command(() => --x1_2);
            DecreaseXout1 = new Command(() => --xout1);
            StartTimer = new Command(() => dispatcherTimer.Start());
            StopTimer = new Command(() => dispatcherTimer.Stop());
        }

        private void ChangeSystemMode()
        {
            PID.IsAuto = ControlSystem.IsAuto;
            SetVisability();
        }

        private void SetTimer()
        {
            _isX10 = !_isX10;
            dispatcherTimer.Interval = new TimeSpan(_isX10 ? 100 : 1000);
        }

        private void SetVisability()
        {
            PIDSettingsVisability = ControlSystem.IsAuto ? Visibility.Visible : Visibility.Hidden;
        }

        private void InitCharts()
        {
            ZSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Z1",
                    Values = new ChartValues<double>(),
                    PointGeometry = null
                },
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
            ControlSystem.Calculate(x1, x2, x1_2, xout1);
            z1 = ControlSystem.Z1;
            z2 = ControlSystem.Z2;
            ZSeriesCollection[0].Values.Add(z1);
            ZSeriesCollection[1].Values.Add(z2);
            if(ZSeriesCollection[0].Values.Count > 100)
            {
                ZSeriesCollection[0].Values.RemoveAt(0);
            }
            if (ZSeriesCollection[1].Values.Count > 100)
            {
                ZSeriesCollection[1].Values.RemoveAt(0);
            }
            time += 0.05;
        }
        #endregion
    }
}

using Jw.MeetingCountdown.Extensions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Jw.MeetingCountdown
{
    /// <summary>
    /// Interaction logic for TimerWindow.xaml
    /// </summary>
    public partial class TimerWindow : Window, INotifyPropertyChanged
    {

        private DispatcherTimer _timer;
        private TimeSpan _meetingStartTimeSpan;

        public TimerWindow(string meetingStartTime, string yearText)
        {
            DataContext = this;
            InitializeComponent();
            MeetingStartTime = meetingStartTime;
            YearText = yearText;
            StartTimer();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.MaximizeToSecondaryMonitor();
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        private string _meetingStartTime;

        public string MeetingStartTime
        {
            get { return _meetingStartTime; }
            set
            {
                _meetingStartTime = value;
                _meetingStartTimeSpan = ConvertToTimeSpan(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MeetingStartTime)));
            }
        }

        private string _yearText;

        public string YearText
        {
            get { return _yearText; }
            set
            {
                _yearText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(YearText)));
            }
        }

        private string _remainingTime;

        public string RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                _remainingTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RemainingTime)));
            }
        }

        private Brush _timerColor = Brushes.Yellow;

        public Brush TimerColor
        {
            get { return _timerColor; }
            set
            {
                _timerColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimerColor)));
            }
        }

        private Visibility _btnRestoreVisibility = Visibility.Visible;

        public Visibility BtnRestoreVisibility
        {
            get { return _btnRestoreVisibility; }
            set
            {
                _btnRestoreVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BtnRestoreVisibility)));
            }
        }

        private Visibility _btnMaximizeVisibility = Visibility.Collapsed;

        public Visibility BtnMaximizeVisibility
        {
            get { return _btnMaximizeVisibility; }
            set
            {
                _btnMaximizeVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BtnMaximizeVisibility)));
            }
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            RemainingTime = GetRemainingTime();
            _timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var remainingTimeSpan = _meetingStartTimeSpan - DateTime.Now.TimeOfDay;
            if (remainingTimeSpan >= TimeSpan.Zero)
                RemainingTime = remainingTimeSpan.ToString(@"mm\:ss");
            if (remainingTimeSpan <= TimeSpan.Zero)
            {
                TimerColor = Brushes.Green;
                _timer.Stop();
            }
        }

        private TimeSpan ConvertToTimeSpan(string strTime)
        {
            var arrTime = strTime.Split(':');
            return new TimeSpan(Convert.ToInt32(arrTime[0]), Convert.ToInt32(arrTime[1]), 0);
        }

        private string GetRemainingTime()
        {
            var remainingTimeSpan = ConvertToTimeSpan(MeetingStartTime) - DateTime.Now.TimeOfDay;
            return $"{remainingTimeSpan.Minutes.ToString().PadLeft(2, '0')}:{remainingTimeSpan.Seconds.ToString().PadLeft(2, '0')}";
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestoreMaximize_Click(object sender, RoutedEventArgs e)
        {
            ToggleRestoreMaximize();
        }

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToggleRestoreMaximize();
        }

        private void ToggleRestoreMaximize()
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                BtnRestoreVisibility = Visibility.Collapsed;
                BtnMaximizeVisibility = Visibility.Visible;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                BtnMaximizeVisibility = Visibility.Collapsed;
                BtnRestoreVisibility = Visibility.Visible;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

    }
}

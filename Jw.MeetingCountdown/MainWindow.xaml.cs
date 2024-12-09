using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Jw.MeetingCountdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            FillFields();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _meetingStartTime;

        public string MeetingStartTime
        {
            get { return _meetingStartTime; }
            set
            {
                _meetingStartTime = value;
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

        private Visibility _invalidTimeErrorVisibility = Visibility.Collapsed;

        public Visibility InvalidTimeErrorVisibility
        {
            get { return _invalidTimeErrorVisibility; }
            set
            {
                _invalidTimeErrorVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvalidTimeErrorVisibility)));
            }
        }

        private string _invalidTimeErrorMessage;

        public string InvalidTimeErrorMessage
        {
            get { return _invalidTimeErrorMessage; }
            set
            {
                _invalidTimeErrorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvalidTimeErrorMessage)));
            }
        }

        private void FillFields() 
        {
            var dateTimeToday = DateTime.Now;

            switch (dateTimeToday.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    MeetingStartTime = "16:15";
                    break;
                case DayOfWeek.Wednesday:
                    MeetingStartTime = "19:30";
                    break;
                default:
                    MeetingStartTime = "19:30";
                    break;
            }

            switch (dateTimeToday.Year)
            {
                case 2024:
                    YearText = "\"Quando fico com medo, ponho minha confiança em ti.\" — Salmo 56:3.";
                    break;
                case 2025:
                    YearText = "\"Deem a Jeová a glória que o seu nome merece.\" — Salmo: 96:8";
                    break;
                default:
                    YearText = string.Empty;
                    break;
            }
        }

        private void ValidateTime()
        {

        }

        private void btnStartTimer_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void tbMeetingStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            if (btnStartTimer.IsEnabled)
            {
                var timerWindow = new TimerWindow(MeetingStartTime, YearText);
                timerWindow.Show();
                Close();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StartTimer();
            }
        }
    }
}

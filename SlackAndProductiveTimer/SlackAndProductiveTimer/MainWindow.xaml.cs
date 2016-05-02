using System;
using System.Windows;
using System.Windows.Threading;


namespace SlackAndProductiveTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private Models.Timer _slackTimer;
        private Models.Timer _productiveTimer;

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            if (_slackTimer.IsActive)
            {
                _slackTimer.incrementMinute(1);
                updateSlackTimer();

            } else if (_productiveTimer.IsActive)
            {
                _productiveTimer.incrementMinute(1);
                updateProductiveTimer();
            }
        }

        private void productiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_productiveTimer.IsActive)
            {
                if (!_slackTimer.IsActive)
                {
                    _timer.Start();
                }
                else
                {
                    setPausedSlackTimerSettings();
                }

                setStartedProdTimerSettings();
            }
            else
            {
                _timer.Stop();
                setPausedProdTimerSettings();
            }
        }

        private void slackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_slackTimer.IsActive)
            {
                if (!_productiveTimer.IsActive)
                {
                    _timer.Start();
                }
                else
                {
                    setPausedProdTimerSettings();
                }

                setStartedSlackTimerSettings();
            }
            else
            {
                _timer.Stop();
                setPausedSlackTimerSettings();
            }
        }

        private void addSlackTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int hour, minute;

            if (int.TryParse(slackHourTextBox.Text, out hour) && int.TryParse(slackMinuteTextBox.Text, out minute) && !_slackTimer.IsActive)
            {
                _slackTimer.incrementMinute(minute);
                _slackTimer.incrementHour(hour);
                  
                updateSlackTimer();
            }
        }

        private void addProductiveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int hour, minute;

            if (int.TryParse(productiveHourTextBox.Text, out hour) && int.TryParse(productiveMinuteTextBox.Text, out minute) && !_productiveTimer.IsActive)
            {
                _productiveTimer.incrementMinute(minute);
                _productiveTimer.incrementHour(hour);

                updateProductiveTimer();
            }
        }

        private void updateSlackTimer()
        {
            slackTimer.Content = _slackTimer.Time;
        }

        private void updateProductiveTimer()
        {
            productiveTimer.Content = _productiveTimer.Time;
        }

        private void setPausedSlackTimerSettings()
        {
            _slackTimer.IsActive = false;
            slackButton.Content = "Start";
        }

        private void setStartedSlackTimerSettings()
        {
            _slackTimer.IsActive = true;
            slackButton.Content = "Pause";
        }

        private void setPausedProdTimerSettings()
        {
            _productiveTimer.IsActive = false;
            productiveButton.Content = "Start";
        }

        private void setStartedProdTimerSettings()
        {
            _productiveTimer.IsActive = true;
            productiveButton.Content = "Pause";
        }

        /// <summary>
        /// Initializes the timer and hour, minute and second variables.
        /// </summary>
        private void init()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(0, 1, 0);
            _slackTimer = new Models.Timer();
            _productiveTimer = new Models.Timer();

            updateSlackTimer();
            updateProductiveTimer();
        }
    }
}

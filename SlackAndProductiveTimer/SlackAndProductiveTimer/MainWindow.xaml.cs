using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SlackAndProductiveTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _slackHour, _slackMinute, _prodHour, _prodMinute;
        private Boolean _productiveTimerOn, _slackTimerOn;
        //private ProdButtonEnum prodButtonJob;
        //private SlackButtonEnum slackButtonJob;

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            if (_slackTimerOn)
            {
                _slackMinute += 1;

                if (_slackMinute == 60)
                {
                    _slackHour += 1;
                    _slackMinute = 0;
                } else if (_slackHour == 24)
                {
                    _slackHour = 0;
                }

                updateSlackTimer();

            } else if (_productiveTimerOn)
            {
                _prodMinute += 1;

                if (_prodMinute == 60)
                {
                    _prodHour += 1;
                    _prodMinute = 0;
                } else if (_prodHour == 24)
                {
                    _prodHour = 0;
                }

                updateProductiveTimer();
            }
        }

        private void productiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_productiveTimerOn)
            {
                if (!_slackTimerOn)
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
            if (!_slackTimerOn)
            {
                if (!_productiveTimerOn)
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

            if (int.TryParse(slackHourTextBox.Text, out hour) && int.TryParse(slackMinuteTextBox.Text, out minute) && !_slackTimerOn)
            {
                if ((_slackMinute += minute) > 59)
                {
                    _slackHour++;
                    _slackMinute -= 60;
                }

                _slackHour += hour;
                
                updateSlackTimer();
            }
        }

        private void addProductiveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int hour, minute;

            if (int.TryParse(productiveHourTextBox.Text, out hour) && int.TryParse(productiveMinuteTextBox.Text, out minute) && !_productiveTimerOn)
            {
                if ((_prodMinute += minute) > 59)
                {
                    _prodHour++;
                    _prodMinute -= 60;
                }

                _prodHour += hour;

                updateProductiveTimer();
            }
        }

        private void updateSlackTimer()
        {
            slackTimer.Content = string.Format("{0} : {1}", _slackHour.ToString().PadLeft(2, '0'),
                _slackMinute.ToString().PadLeft(2, '0'));
        }

        private void updateProductiveTimer()
        {
            productiveTimer.Content = string.Format("{0} : {1}", _prodHour.ToString().PadLeft(2, '0'),
                _prodMinute.ToString().PadLeft(2, '0'));
        }

        private void setPausedSlackTimerSettings()
        {
            _slackTimerOn = false;
            slackButton.Content = "Start";
            //slackButtonJob = SlackButtonEnum.Start;
        }

        private void setStartedSlackTimerSettings()
        {
            _slackTimerOn = true;
            slackButton.Content = "Pause";
            //slackButtonJob = SlackButtonEnum.Pause;
        }

        private void setPausedProdTimerSettings()
        {
            _productiveTimerOn = false;
            productiveButton.Content = "Start";
            //prodButtonJob = ProdButtonEnum.Start;
        }

        private void setStartedProdTimerSettings()
        {
            _productiveTimerOn = true;
            productiveButton.Content = "Pause";
            //prodButtonJob = ProdButtonEnum.Pause;
        }

        /// <summary>
        /// Initializes the timer and hour, minute and second variables.
        /// </summary>
        private void init()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(0, 1, 0);

            _slackHour = _slackMinute = _prodHour = _prodMinute = 0;
            _productiveTimerOn = _slackTimerOn = false;
            //prodButtonJob = ProdButtonEnum.Start;
            //slackButtonJob = SlackButtonEnum.Start;
        }
    }
}

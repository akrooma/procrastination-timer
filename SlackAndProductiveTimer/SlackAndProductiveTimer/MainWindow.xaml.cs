using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private int slackHour, slackMinute, prodHour, prodMinute;
        private Boolean productiveTimerOn, slackTimerOn;
        private ProdButtonEnum prodButtonJob;
        private SlackButtonEnum slackButtonJob;

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            if (slackTimerOn)
            {
                slackMinute += 1;

                if (slackMinute == 60)
                {
                    slackHour += 1;
                    slackMinute = 0;
                }

                slackTimer.Content = string.Format("{0}:{1}", slackHour.ToString().PadLeft(2, '0'), 
                    slackMinute.ToString().PadLeft(2, '0'));

            } else if (productiveTimerOn)
            {
                prodMinute += 1;

                if (prodMinute == 60)
                {
                    prodHour += 1;
                    prodMinute = 0;
                }

                productiveTimer.Content = string.Format("{0}:{1}", prodHour.ToString().PadLeft(2, '0'),
                    prodMinute.ToString().PadLeft(2, '0'));
            }
        }

        private void productiveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (prodButtonJob)
            {
                case ProdButtonEnum.Start:
                    if (!slackTimerOn)
                    {
                        timer.Start();
                    }
                    else
                    {
                        setPausedSlackTimerSettings();
                    }

                    setStartedProdTimerSettings();
                    break;

                case ProdButtonEnum.Pause:
                    timer.Stop();
                    setPausedProdTimerSettings();
                    
                    break;
            }
        }

        private void slackButton_Click(object sender, RoutedEventArgs e)
        {
            switch (slackButtonJob)
            {
                case SlackButtonEnum.Start:
                    if (!productiveTimerOn)
                    {
                        timer.Start();
                    }
                    else
                    {
                        setPausedProdTimerSettings();
                    }

                    setStartedSlackTimerSettings();
                    break;

                case SlackButtonEnum.Pause:
                    timer.Stop();
                    setPausedSlackTimerSettings();

                    break;
            }
        }

        private void setPausedSlackTimerSettings()
        {
            slackTimerOn = false;
            slackButton.Content = "Start";
            slackButtonJob = SlackButtonEnum.Start;
        }

        private void setStartedSlackTimerSettings()
        {
            slackTimerOn = true;
            slackButton.Content = "Pause";
            slackButtonJob = SlackButtonEnum.Pause;
        }

        private void setPausedProdTimerSettings()
        {
            productiveTimerOn = false;
            productiveButton.Content = "Start";
            prodButtonJob = ProdButtonEnum.Start;
        }

        private void setStartedProdTimerSettings()
        {
            productiveTimerOn = true;
            productiveButton.Content = "Pause";
            prodButtonJob = ProdButtonEnum.Pause;
        }

        /// <summary>
        /// Initializes the timer and hour, minute and second variables.
        /// </summary>
        private void init()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 1, 0);

            slackHour = slackMinute = prodHour = prodMinute = 0;
            productiveTimerOn = slackTimerOn = false;
            prodButtonJob = ProdButtonEnum.Start;
            slackButtonJob = SlackButtonEnum.Start;
        }
    }
}

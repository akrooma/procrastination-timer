using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackAndProductiveTimer.Models
{
    public class Timer
    {
        private int _hour;
        private int _minute;

        public Timer()
        {
            _hour = 0;
            _minute = 0;
        }

        public void incrementMinute(int minute)
        {
            if ((_minute += minute) > 59)
            {
                _hour += _minute / 60;
                _minute = _minute % 60;
            }
        }

        public void incrementHour(int hour)
        {
            if ((_hour += hour) > 23)
            {
                _hour = _hour % 24;
            }
        }

        public string Time
        {
            get
            {
                return string.Format("{0} : {1}", _hour.ToString().PadLeft(2, '0'),
                _minute.ToString().PadLeft(2, '0'));
            }
        }
    }
}

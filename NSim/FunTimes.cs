using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public static class FunTimes
    {
        public static TimeSpan Seconds(this int val)
        {
            return new TimeSpan(val*TimeSpan.TicksPerSecond);
        }

        public static TimeSpan Minutes(this int val)
        {
            return new TimeSpan(val * TimeSpan.TicksPerMinute);
        }

        public static TimeSpan Hours(this int val)
        {
            return new TimeSpan(val * TimeSpan.TicksPerHour);
        }

        public static TimeSpan Days(this int val)
        {
            return new TimeSpan(val * TimeSpan.TicksPerDay);
        }

        public static TimeSpan Weeks(this int val)
        {
            return new TimeSpan(val * TimeSpan.TicksPerDay * 7);
        }

        public static DateTime From(this TimeSpan interval, DateTime d)
        {
            return d + interval;
        }

        public static TimeSpan Seconds(this double val)
        {
            return TimeSpan.FromSeconds(val);
        }
    }
}

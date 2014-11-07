using System;

namespace MultiwinService
{
    public static class DateTimeExtensions
    {
        public static bool IsHourlyTime(this DateTime time, DateTime? lastTime, TimeSpan span)
        {
            var min = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0).Add(span);
            var max = min.AddHours(1);
            if (lastTime == null)
            {
                if (time >= min && time < max)
                {
                    return true;
                }
            }
            else
            {
                if (time >= min && time < max && min > lastTime.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDailyTime(this DateTime time, DateTime? lastTime, TimeSpan span)
        {
            var min = time.Date.Add(span);
            var max = min.AddDays(1);
            if (lastTime == null)
            {
                if (time >= min && time < max)
                {
                    return true;
                }
            }
            else
            {
                if (time >= min && time < max && min > lastTime.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsWeeklyTime(this DateTime time, DateTime? lastTime, TimeSpan span, DayOfWeek week)
        {
            var dayNow = time.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) time.DayOfWeek;
            var dayWeek = week == DayOfWeek.Sunday ? 7 : (int) week;
            var min = time.Date.AddDays(dayWeek - dayNow).Add(span);
            var max = min.AddDays(7);
            if (lastTime == null)
            {
                if (time >= min && time < max)
                {
                    return true;
                }
            }
            else
            {
                if (time >= min && time < max && min > lastTime.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsMonthlyTime(this DateTime time, DateTime? lastTime, TimeSpan span)
        {
            var min = time.Date.AddDays(1 - time.Date.Day).Add(span);
            var max = min.AddMonths(1);
            if (lastTime == null)
            {
                if (time >= min && time < max)
                {
                    return true;
                }
            }
            else
            {
                if (time >= min && time < max && min > lastTime.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

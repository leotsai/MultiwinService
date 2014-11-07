using System;
using NUnit.Framework;

namespace MultiwinService.UnitTests.Infrastructure
{
    internal class 时间启动频率扩展测试
    {
        #region hourly

        [Test]
        public void Hourly_没有上次执行时间_时间未到_应该在下个小时执行()
        {
            var isTime = DateTime.Now.IsHourlyTime(null, new TimeSpan(0, 0, DateTime.Now.Minute + 1, 0));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Hourly_没有上次执行时间_时间已到_应该立即执行()
        {
            var isTime = DateTime.Now.IsHourlyTime(null, new TimeSpan(0, 0, DateTime.Now.Minute - 1, 0));
            Assert.AreEqual(true, isTime);
        }

        [Test]
        public void Hourly_有上次执行时间_刚刚执行完_当前不执行()
        {
            var isTime = DateTime.Now.IsHourlyTime(DateTime.Now.AddSeconds(-1), new TimeSpan(0, 0, DateTime.Now.Minute -1, 0));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Hourly_有上次执行时间_执行完很久了_当前立即执行()
        {
            var isTime = DateTime.Now.IsHourlyTime(DateTime.Now.AddDays(-1), new TimeSpan(0, 0, DateTime.Now.Minute, 0));
            Assert.AreEqual(true, isTime);
        }

        #endregion

        #region daily

        [Test]
        public void Daily_没有上次执行时间_时间未到_应该在第二天执行()
        {
            var isTime = DateTime.Now.IsDailyTime(null, new TimeSpan(0, DateTime.Now.Hour + 1, 0, 0));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Daily_没有上次执行时间_时间已到_应该立即执行()
        {
            var isTime = DateTime.Now.IsDailyTime(null, new TimeSpan(0, DateTime.Now.Hour - 1, 0, 0));
            Assert.AreEqual(true, isTime);
        }

        [Test]
        public void Daily_有上次执行时间_刚刚执行完_当前不执行()
        {
            var isTime = DateTime.Now.IsDailyTime(DateTime.Now.AddSeconds(-1), new TimeSpan(0, DateTime.Now.Hour, 0, 0));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Daily_有上次执行时间_执行完很久了_当前立即执行()
        {
            var isTime = DateTime.Now.IsDailyTime(DateTime.Now.AddMonths(-1), new TimeSpan(0, DateTime.Now.Hour, 0, 0));
            Assert.AreEqual(true, isTime);
        }

        #endregion

        #region weekly

        [Test]
        public void Weekly_没有上次执行时间_时间未到_应该在下个星期执行()
        {
            var time = new DateTime(2014, 6, 20, 8, 0, 0);//friday 8:00 am
            var isTime = time.IsWeeklyTime(null, new TimeSpan(0, 0, 0, 0), DayOfWeek.Saturday);
            Assert.AreEqual(false, isTime);
            isTime = time.IsWeeklyTime(null, new TimeSpan(0, 8, 0, 1), DayOfWeek.Friday);
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Weekly_没有上次执行时间_时间已到_应该立即执行()
        {
            var time = new DateTime(2014, 6, 20, 8, 0, 0);//friday 8:00 am
            var isTime = time.IsWeeklyTime(null, new TimeSpan(0, 0, 0, 0), DayOfWeek.Friday);
            Assert.AreEqual(true, isTime);
            isTime = time.IsWeeklyTime(null, new TimeSpan(0, 8, 0, 0), DayOfWeek.Friday);
            Assert.AreEqual(true, isTime);
        }

        [Test]
        public void Weekly_有上次执行时间_刚刚执行完_当前不执行()
        {
            var time = new DateTime(2014, 6, 20, 8, 0, 0);//friday 8:00 am
            var isTime = time.IsWeeklyTime(time.AddDays(-6), new TimeSpan(0, 8, 0, 1), DayOfWeek.Friday);
            Assert.AreEqual(false, isTime);
            isTime = time.IsWeeklyTime(time, new TimeSpan(0, 8, 0, 1), DayOfWeek.Friday);
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Weekly_有上次执行时间_执行完很久了_当前立即执行()
        {
            var time = new DateTime(2014, 6, 20, 8, 0, 0);//friday 8:00 am
            var isTime = time.IsWeeklyTime(time.AddDays(-10), new TimeSpan(0, 0, 0, 0), DayOfWeek.Friday);
            Assert.AreEqual(true, isTime);
            isTime = time.IsWeeklyTime(time.AddDays(-7), new TimeSpan(0, 8, 0, 0), DayOfWeek.Friday);
            Assert.AreEqual(true, isTime);
        }

        #endregion

        #region monthly

        [Test]
        public void Monthly_没有上次执行时间_时间未到_应该在下个月执行()
        {
            var time = new DateTime(2014, 1, 1, 20, 0, 0);
            var isTime = time.IsMonthlyTime(null, new TimeSpan(0, 20, 0, 1));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Monthly_没有上次执行时间_时间已到_应该立即执行()
        {
            var time = new DateTime(2014, 1, 1, 20, 0, 0);
            var isTime = time.IsMonthlyTime(null, new TimeSpan(0, 20, 0, 0));
            Assert.AreEqual(true, isTime);
        }

        [Test]
        public void Monthly_有上次执行时间_刚刚执行完_当前不执行()
        {
            var time = new DateTime(2014, 1, 1, 20, 0, 0);
            var isTime = time.IsMonthlyTime(time.AddDays(-1), new TimeSpan(0, 20, 0, 1));
            Assert.AreEqual(false, isTime);
        }

        [Test]
        public void Monthly_有上次执行时间_执行完很久了_当前立即执行()
        {
            var time = new DateTime(2014, 1, 1, 20, 0, 0);
            var isTime = time.IsMonthlyTime(time.AddYears(-1), new TimeSpan(0, 20, 0, 0));
            Assert.AreEqual(true, isTime);
        }

        #endregion
    }
}

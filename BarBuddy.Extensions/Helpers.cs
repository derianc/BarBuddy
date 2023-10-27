namespace BarBuddy.Extensions
{
    public static class Helpers
    {
        public static DateTime RandomDOB(this DateTime date)
        {
            Random random = new Random();
            DateTime start = new DateTime(2002, 1, 1);
            int range = (DateTime.Today - start).Days;

            int randomHour = random.Next(0, 24);
            int randomMinute = random.Next(0, 60);
            int randomSecond = random.Next(0, 60);

            var randomDate = start.AddDays(random.Next(range));

            var newDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, randomHour, randomMinute, randomSecond);

            return newDate;
        }
    }
}
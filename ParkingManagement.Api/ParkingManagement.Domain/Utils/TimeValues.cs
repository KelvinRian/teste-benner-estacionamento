namespace ParkingManagement.Domain.Utils
{
    public static class TimeValues
    {
        public static readonly TimeSpan HalfAnHour = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan OneHour = TimeSpan.FromHours(1);
        public static readonly TimeSpan ToleranceTime = TimeSpan.FromMinutes(10);
    }
}

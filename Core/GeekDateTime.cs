namespace Core;

/// <summary>
///     Use this class instead of DateTime,
///     allows for testing (set the UtcDateTime / LocalDateTime to a fixed time)
/// </summary>
public static class GeekDateTime
{
    private static Func<DateTime> _utcNowFunc = () => DateTime.UtcNow;

    private static Func<DateTime> _systemNowFunc = () =>
        TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc.Invoke(), TimeZoneInfo.FindSystemTimeZoneById("UTC"));

    public static DateTime UtcNow => _utcNowFunc.Invoke();
    public static DateTime Now => _systemNowFunc.Invoke();
    public static DateTime Today => _systemNowFunc.Invoke().Date;
    public static DateTime UtcToday => _utcNowFunc.Invoke().Date;

    public static void SetUtcDateTime(DateTime dateTimeNow)
    {
        _utcNowFunc = () => dateTimeNow;
    }

    public static void SetLocalDateTime(string timeZoneId)
    {
        _systemNowFunc = () =>
            TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc.Invoke(), TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
    }

    public static void ResetDateTime()
    {
        _utcNowFunc = () => DateTime.UtcNow;
        _systemNowFunc = () => DateTime.Now;
    }

    public static uint YearsPassed(DateTime fromDate, DateTime toDate)
    {
        if (fromDate > toDate)
            return 0;

        var yearsPassed = toDate.Year - fromDate.Year;

        if (toDate.Month < fromDate.Month ||
            (toDate.Month == fromDate.Month && toDate.Day < fromDate.Day))
            yearsPassed -= 1;

        return (uint)yearsPassed;
    }

    // TODO - Make this a "Date Only" method?
    public static int YearsPassedInTime(DateTime fromDate, DateTime toDate)
    {
        return fromDate <= toDate
            ? (int)YearsPassed(fromDate, toDate)
            : -1 * (int)YearsPassed(toDate, fromDate);
    }
}
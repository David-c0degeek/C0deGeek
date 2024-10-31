namespace Core;

/// <summary>
/// Provides a mockable wrapper around DateTime functionality for testing purposes.
/// This class should be used instead of DateTime directly to enable testing with fixed times.
/// </summary>
public static class GeekDateTime
{
    private static Func<DateTime> _utcNowFunc = () => DateTime.UtcNow;
    private static Func<DateTime> _systemNowFunc = () => 
        TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc.Invoke(), TimeZoneInfo.FindSystemTimeZoneById("UTC"));
    
    private static readonly object TimeLock = new();
    private static TimeZoneInfo _currentTimeZone = TimeZoneInfo.Utc;

    /// <summary>
    /// Gets the current UTC date and time. Can be mocked for testing.
    /// </summary>
    public static DateTime UtcNow => _utcNowFunc.Invoke();

    /// <summary>
    /// Gets the current system local date and time. Can be mocked for testing.
    /// </summary>
    public static DateTime Now => _systemNowFunc.Invoke();

    /// <summary>
    /// Gets the current system local date at midnight. Can be mocked for testing.
    /// </summary>
    public static DateTime Today => _systemNowFunc.Invoke().Date;

    /// <summary>
    /// Gets the current UTC date at midnight. Can be mocked for testing.
    /// </summary>
    public static DateTime UtcToday => _utcNowFunc.Invoke().Date;

    /// <summary>
    /// Sets a fixed UTC date and time for testing purposes.
    /// </summary>
    /// <param name="dateTimeNow">The fixed UTC date and time to use.</param>
    public static void SetUtcDateTime(DateTime dateTimeNow)
    {
        _utcNowFunc = () => dateTimeNow;
    }

    /// <summary>
    /// Sets the time zone for local time calculations.
    /// </summary>
    /// <param name="timeZoneId">The time zone identifier to use.</param>
    public static void SetLocalDateTime(string timeZoneId)
    {
        _systemNowFunc = () =>
            TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc.Invoke(), TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
    }

    /// <summary>
    /// Resets the date and time functions to use actual system time.
    /// </summary>
    public static void ResetDateTime()
    {
        _utcNowFunc = () => DateTime.UtcNow;
        _systemNowFunc = () => DateTime.Now;
    }

    /// <summary>
    /// Calculates the number of complete years between two dates.
    /// </summary>
    /// <param name="fromDate">The starting date.</param>
    /// <param name="toDate">The ending date.</param>
    /// <returns>The number of complete years between the dates, or 0 if fromDate is later than toDate.</returns>
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

    /// <summary>
    /// Calculates the number of years between two dates, including negative values for reverse calculations.
    /// </summary>
    /// <param name="fromDate">The starting date.</param>
    /// <param name="toDate">The ending date.</param>
    /// <returns>The number of years between the dates (negative if fromDate is later than toDate).</returns>
    public static int YearsPassedInTime(DateTime fromDate, DateTime toDate)
    {
        return fromDate <= toDate
            ? (int)YearsPassed(fromDate, toDate)
            : -1 * (int)YearsPassed(toDate, fromDate);
    }
    
    /// <summary>
    /// Sets the time zone safely, with validation.
    /// </summary>
    public static void SetTimeZone(string timeZoneId)
    {
        try
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            lock (TimeLock)
            {
                _currentTimeZone = timeZone;
            }
        }
        catch (TimeZoneNotFoundException ex)
        {
            throw new ArgumentException($"Invalid time zone ID: {timeZoneId}", nameof(timeZoneId), ex);
        }
    }

    /// <summary>
    /// Executes an action with a temporary fixed time, then restores the previous time.
    /// </summary>
    public static T ExecuteWithFixedTime<T>(DateTime fixedTime, Func<T> action)
    {
        var original = _utcNowFunc;
        try
        {
            SetUtcDateTime(fixedTime);
            return action();
        }
        finally
        {
            _utcNowFunc = original;
        }
    }

    /// <summary>
    /// Gets the start of the current business day in the current time zone.
    /// </summary>
    public static DateTime StartOfBusinessDay
    {
        get
        {
            var local = TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc(), _currentTimeZone);
            return local.Date.AddHours(9); // Assuming 9 AM start
        }
    }

    /// <summary>
    /// Gets the end of the current business day in the current time zone.
    /// </summary>
    public static DateTime EndOfBusinessDay
    {
        get
        {
            var local = TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc(), _currentTimeZone);
            return local.Date.AddHours(17); // Assuming 5 PM end
        }
    }

    /// <summary>
    /// Determines if the current time is within business hours.
    /// </summary>
    public static bool IsBusinessHours
    {
        get
        {
            var local = TimeZoneInfo.ConvertTimeFromUtc(_utcNowFunc(), _currentTimeZone);
            return local.Hour is >= 9 and < 17 && local.DayOfWeek != DayOfWeek.Saturday && local.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
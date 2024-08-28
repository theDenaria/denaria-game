using System;
using System.Globalization;

public static class DateUtility
{
    private static readonly DateTime EPOCH_START_DATE_TIME = new DateTime(1970, 1, 1, 0, 0, 0); //TODO: We should try DateTime.UnixEpoch instead of hard coded
    private static readonly string[] DATE_TIME_FORMATS = new string[]
    {
        "yyyy-MM-dd",
        "M/d/yyyy",
        "MM/dd/yyyy",
        "yyyy/MM/dd",
        "dd/MM/yyyy",
        "dd-MMM-yyyy",
        "MMM-dd-yyyy",
        "yyyy-MM-ddTHH:mm:ss"
    };

    public static bool TryParse(string dateString, out DateTime date)
    {
        return DateTime.TryParseExact(dateString, DATE_TIME_FORMATS, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
    }

    public static long GetCurrentEpochSeconds()
    {
        return Convert.ToInt64((DateTime.Now - EPOCH_START_DATE_TIME).TotalSeconds);
    }

    public static double GetCurrentEpochSecondsPrecise()
    {
        return (DateTime.Now - EPOCH_START_DATE_TIME).TotalSeconds;
    }

    public static int CalculateTimeDifference(long fromEpochSeconds, long toEpochSeconds)
    {
        return Convert.ToInt32(toEpochSeconds - fromEpochSeconds);
    }

    public static float CalculateTimeDifference(double fromEpochSeconds, double toEpochSeconds)
    {
        return Convert.ToSingle(toEpochSeconds - fromEpochSeconds);
    }

}

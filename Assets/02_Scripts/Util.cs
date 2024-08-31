using System.Globalization;
using UnityEngine;

public static class Util
{
    public static string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3500) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string timeString = "";

        if (hours > 0)
        {
            timeString += hours.ToString("00") + ":";
        }

        timeString += minutes.ToString("00") + ":" + seconds.ToString("00");
        return timeString;
    }

    public static string FormatIntToReadableString(int number)
    {
        return number.ToString("N0", CultureInfo.InvariantCulture);
    }
}

using System;
using System.Collections.Generic;

public class DateTimeComparer : IComparer<DateTime?>
{
    public int Compare(DateTime? x, DateTime? y)
    {
        DateTime nx = x ?? DateTime.MaxValue;
        DateTime ny = y ?? DateTime.MaxValue;

        return nx.CompareTo(ny);
    }
}
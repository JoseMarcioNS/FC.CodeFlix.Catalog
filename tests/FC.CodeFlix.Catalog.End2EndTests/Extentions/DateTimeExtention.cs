using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.End2EndTests.Extentions
{
    public static class DateTimeExtention
    {
        public static DateTime TrimMilliseconds(this DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                0,
                dateTime.Kind);
        }
    }
}

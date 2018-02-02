using System;

namespace Petronas.SampleApp.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertToDate(this string strDate)
        {
            return DateTime.ParseExact(strDate, Constants.DefaultDateFormat,
                System.Globalization.CultureInfo.InvariantCulture);
        }

    }
}
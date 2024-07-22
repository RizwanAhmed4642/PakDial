using PAKDial.Domains.Common;
using System;

namespace PAKDial.Common
{
    public static class CommonDateFormat
    {
        //26th May, 2018
        public static string GetDateInMonthString(DateTime date)
        {
            string Day = date.Day.ToString()+"th";
            string Month = GetMonth(date.Month);
            string Year = date.Year.ToString();
            string FullYear = Day +" "+Month+", "+Year;
            return FullYear;
        }
        public static string GetMonth(int Month)
        {
            var Months = ((MonthsinOneYear)Month).ToString();
            return Months;
        }
    }

}

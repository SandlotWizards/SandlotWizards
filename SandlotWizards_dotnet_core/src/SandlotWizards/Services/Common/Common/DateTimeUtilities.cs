using System;

namespace SandlotWizards.Common
{
    public static class DateTimeUtilities
    {

        public static string GetCurrentDate()
        {
            return GeneralUtilities.addZeros(DateTime.Now.Year.ToString(), 4, true) + "-" +
                   GeneralUtilities.addZeros(DateTime.Now.Month.ToString().Trim(), 2, true) +
                   GeneralUtilities.addZeros(DateTime.Now.Day.ToString().Trim(), 2, true);
        }

        public static string ConvertYYYYMMDDtoMMDDYY(string YYYYMMDD)
        {
            return YYYYMMDD.Substring(5, 2) + "/" + YYYYMMDD.Substring(8, 2) + "/" + YYYYMMDD.Substring(2, 2);
        }
        public static string ConvertDateTimetoYYYYMMDD(DateTime dt)
        {
            return GeneralUtilities.addZeros(dt.Year.ToString(), 4, true) + "-" + GeneralUtilities.addZeros(dt.Month.ToString(), 2, true) + "-" + GeneralUtilities.addZeros(dt.Day.ToString(), 2, true);
        }

        public static string ConvertMMDDYYYY_TimeToYYYYMMDD(string MMDDYYYY_Time)
        {
            if (MMDDYYYY_Time.Trim().Length == 0) return "0001-01-01";

            string[] dateArray = MMDDYYYY_Time.Split(' ')[0].Split('/');
            return GeneralUtilities.addZeros(dateArray[2], 4, true) + "-" + GeneralUtilities.addZeros(dateArray[0], 2, true) + "-" + GeneralUtilities.addZeros(dateArray[1], 2, true);
        }

        public static string ConvertMMDDYYYYToYYYYMMDD(string MMDDYYYY)
        {
            string[] dateArray = MMDDYYYY.Split('/');
            return GeneralUtilities.addZeros(dateArray[2], 4, true) + "-" + GeneralUtilities.addZeros(dateArray[0], 2, true) + "-" + GeneralUtilities.addZeros(dateArray[1], 2, true);
        }
        public static string stripTimeFromDateString(string source)
        {
            return source.Split(' ')[0];
        }
    }
}

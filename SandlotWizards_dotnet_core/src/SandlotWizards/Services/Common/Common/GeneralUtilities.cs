using System;
using System.Data;

namespace SandlotWizards.Common
{
    public static class GeneralUtilities
    {
        public static DataColumn CreateDataColumn(string columnName, Type datatype, string columnHeader)
        {
            DataColumn col = new DataColumn(columnName, datatype);
            col.Caption = columnHeader;
            return col;
        }

        public static string ConvertDataSetToCSV(DataSet dataset, bool useDoubleQuotesAroundColumnValue)
        {
            string csvString = "";

            string headerString = "";
            foreach (DataColumn dataColumn in dataset.Tables[0].Columns)
            {
                if (headerString.Length > 0) { headerString += ","; }
                string columnName = dataColumn.Caption;
                headerString += $"\"{columnName}\"";
            }
            headerString += "\r\n";
            csvString += headerString;

            foreach (DataRow dataRow in dataset.Tables[0].Rows)
            {
                string lineString = "";
                foreach (DataColumn dataColumn in dataset.Tables[0].Columns)
                {
                    if (lineString.Length > 0) { lineString += ","; }
                    string columnValue = dataRow[dataColumn.ColumnName].ToString();
                    string dataType = dataRow[dataColumn.ColumnName].GetType().Name;
                    if (dataType.Equals("String") || dataType.Equals("DateTime"))
                    {
                        if (useDoubleQuotesAroundColumnValue) { lineString += $"\"{columnValue}\""; }
                        else { lineString += $"{columnValue}"; }

                    }
                    else
                    {
                        lineString += $"{columnValue}";
                    }
                }
                lineString += "\r\n";
                csvString += lineString;
            }


            return csvString;
        }

        public static string addZeros(string source, int noOfZeros, bool trimToLength)
        {
            while (source.Length < noOfZeros) { source = "0" + source; }

            while (trimToLength && source.Length > noOfZeros)
            {
                source = source.Substring(1, source.Length - 1);
            }
            return source;
        }

        public static string ConvertStringToLowerPlusFirstCaps(string sourceString)
        {
            sourceString = sourceString.ToLower();
            if (sourceString.Length < 2)
            {
                sourceString = sourceString.ToUpper();
            }
            else
            {
                sourceString = sourceString.Substring(0, 1).ToUpper() + sourceString.ToLower().Substring(1, sourceString.Length - 1);
            }

            return sourceString;
        }
    }
}

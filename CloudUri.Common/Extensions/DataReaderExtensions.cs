using System.Data;

namespace CloudUri.Common.Extensions
{
    /// <summary>
    /// Extension methods for data reader
    /// </summary>
    public static class DataReaderExtensions
    {
         /// <summary>
         /// Checks the index for dbnull and returns string if it isn't
         /// </summary>
         /// <param name="dataReader">Data reader to read</param>
         /// <param name="index">Index of string</param>
         /// <returns>String or null</returns>
         public static string GetNullableString(this IDataReader dataReader, int index)
         {
             return dataReader.IsDBNull(index) ? null : dataReader.GetString(index);
         }

         /// <summary>
         /// Checks the index for dbnull and returns int if it isn't
         /// </summary>
         /// <param name="dataReader">Data reader to read</param>
         /// <param name="index">Index of string</param>
         /// <returns>String or null</returns>
         public static int? GetNullableInt(this IDataReader dataReader, int index)
         {
             return dataReader.IsDBNull(3) ? (int?) (null) : dataReader.GetInt32(3);
         }
    }
}
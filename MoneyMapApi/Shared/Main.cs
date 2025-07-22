using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Menassah.Shared
{
    public class Main
    {
        public static int DefaultCompanyID { get; set; }

        #region ClientEncryptionKeys
        public static string Key1 = "VSOFTWEB";
        public static string Key2 = "ADMNLOGN";
        #endregion


        #region Connection
        public static string DataProvider { get; set; }
        public static string ConnectionString { get; set; }
        #endregion

        #region General
        public static bool IsDate(Object Date)
        {
            string strDate = Date.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region UploadExcel
        public static bool AddExtraFeild(DataTable Data, string ColumnName, Type ColumnType)
        {
            try
            {
                if (!Data.Columns.Contains(ColumnName))
                {
                    Data.Columns.Add(ColumnName, ColumnType);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        public static IEnumerable<Dictionary<string, object>> Serialize(DataTable dt)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < dt.Columns.Count; i++)
                cols.Add(dt.Columns[i].ColumnName);

            for (int i = 0; i < dt.Rows.Count; i++)
                results.Add(SerializeRow(cols, dt.Rows[i]));


            return results;
        }
        public static Dictionary<string, object> SerializeRow(IEnumerable<string> cols, DataRow row)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, row[col]);
            return result;
        }


    }
}

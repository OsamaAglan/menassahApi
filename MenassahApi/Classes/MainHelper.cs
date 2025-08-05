using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using MenassahApi.DL;
using System.Text;
using System.Threading.Tasks;
using Menassah.Repository;

namespace VSoftCore
{
    public class MainHelper : IMainHelper
    {

        public GeneralResponse GetOkResponse(string Message, object Data)
        {
            GeneralResponse resonse = new GeneralResponse
            {
                ID = "0",
                Message = Message,
                Success = true,
                Data = Data
            };


            return resonse;
        }
        public GeneralResponse GetBadResponse(string Message, object Data)
        {
            GeneralResponse resonse = new GeneralResponse
            {
                ID = "0",
                Message = Message,
                Success = false,
                Data = Data
            };


            return resonse;
        }
        public GeneralResponse GetException(Exception ex)
        {
            string ErrorMessage = "";
            object Data = null;
#if DEBUG
            ErrorMessage = ex.Message;
            Data = ex.StackTrace;
#endif
            return GetBadResponse(ErrorMessage, Data);
        }

        #region Serialize
        public IEnumerable<Dictionary<string, object>> Serialize(DataTable dt)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < dt.Columns.Count; i++)
                cols.Add(ToCamelCase(dt.Columns[i].ColumnName));

            for (int i = 0; i < dt.Rows.Count; i++)
                results.Add(SerializeRow(cols, dt.Rows[i]));

            return results;
        }
        public Dictionary<string, object> SerializeRow(IEnumerable<string> cols, DataRow row)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                result.Add(col, (row[col] == DBNull.Value) ? null : row[col]);
            }

            return result;
        }
        public IEnumerable<Dictionary<string, object>> Serialize(DataRow[] dataRows)
        {
            try
            {
                var results = new List<Dictionary<string, object>>();
                var cols = new List<string>();
                foreach (DataColumn c in dataRows[0].Table.Columns)
                {
                    cols.Add(ToCamelCase(c.ColumnName));
                }

                for (int i = 0; i < dataRows.Length; i++)
                {
                    results.Add(SerializeRow(cols, dataRows[i]));
                }
                return results;

            }
            catch (Exception)
            {

                return null;
            }


        }
        public Dictionary<string, object> SerializeFirst(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return null;
            }

            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < dt.Columns.Count; i++)
                cols.Add(ToCamelCase(dt.Columns[i].ColumnName));

            results.Add(SerializeRow(cols, dt.Rows[0]));

            return results.FirstOrDefault();
        }
        private string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var sb = new StringBuilder();
            bool wasPrevUpper = false;
            for (int i = 0; i < str.Length; i++)
            {
                char currentChar = str[i];

                if (i == 0)
                {
                    sb.Append(char.ToLower(currentChar));
                    wasPrevUpper = char.IsUpper(currentChar);
                }
                else
                {
                    if (char.IsUpper(currentChar))
                    {
                        if (wasPrevUpper)
                        {
                            sb.Append(char.ToLower(currentChar));
                        }
                        else
                        {
                            sb.Append(currentChar);
                        }
                        wasPrevUpper = true;
                    }
                    else
                    {
                        sb.Append(currentChar);
                        wasPrevUpper = false;
                    }
                }
            }

            return sb.ToString();
        }
        #endregion

        public string GetLanguage(HttpRequest request)
        {
            string Language = "";
            try
            {
                if (request.Headers.ContainsKey("Language"))
                    Language = request.Headers["Language"].First();
                else
                    Language = "en";

            }
            catch (Exception)
            {

            }
            return Language;
        }
        public string GetAuthorization(HttpRequest request)
        {
            string Authorization = "";
            try
            {

                if (request.Headers.ContainsKey("Authorization"))
                    Authorization = request.Headers["Authorization"].First();
                if (string.IsNullOrEmpty(Authorization))
                    Authorization = "";
            }
            catch (Exception)
            {
                Authorization = "";
            }

            return Authorization;
        }
        public string GetAccessToken(HttpRequest request)
        {
            string AccessToken = GetAuthorization(request);
            try
            {
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    if (request.Headers.ContainsKey("AccessToken"))
                        AccessToken = request.Headers["AccessToken"].First();
                }

            }
            catch (Exception)
            {

            }
            return AccessToken;
        }
        public int GetAdminUserIDByAccessToken(string AccessToken, int CompanyID)
        {
            return 0;
        }


        public int GetAdminIDByAccessToken(string AccessToken, int CompanyID)
        {

            return 0;

        }
        public string GetBrowserName(HttpRequest request)
        {
            string BrowserName = "";
            try
            {
                if (request.Headers.ContainsKey("User-Agent"))
                    BrowserName = request.Headers["User-Agent"].First();

            }
            catch (Exception)
            {

            }
            return BrowserName;
        }

        public int GetAdminUserIDFromRequest(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public int GetAdminIDFromRequest(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public int GetCompanyID(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

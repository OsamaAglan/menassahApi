using Microsoft.AspNetCore.Http;
using MenassahApi.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menassah.Repository
{
    public partial interface IMainHelper
    {
        GeneralResponse GetOkResponse(string Message, object Data);
        GeneralResponse GetBadResponse(string Message, object Data);
        GeneralResponse GetException(Exception ex);
        IEnumerable<Dictionary<string, object>> Serialize(DataTable dt);
        Dictionary<string, object> SerializeRow(IEnumerable<string> cols, DataRow row);
        IEnumerable<Dictionary<string, object>> Serialize(DataRow[] dataRows);
        string GetLanguage(HttpRequest request);
        Dictionary<string, object> SerializeFirst(DataTable dt);
        int GetCompanyID(HttpRequest request);
        string GetAuthorization(HttpRequest request);
        string GetAccessToken(HttpRequest request);
        public int GetAdminUserIDByAccessToken(string AccessToken, int CompanyID);
        int GetAdminUserIDFromRequest(HttpRequest request);
        int GetAdminIDByAccessToken(string AccessToken, int CompanyID);
        int GetAdminIDFromRequest(HttpRequest request);
        string GetBrowserName(HttpRequest request);

    }
}

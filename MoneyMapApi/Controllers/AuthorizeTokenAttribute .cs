using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

public class AuthorizeTokenAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;

        // قراءة التوكن من الهيدر
        var token = request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        token = token.Substring("Bearer ".Length).Trim();

        // تحقق من صحة التوكن في قاعدة البيانات
        var isValid = ValidateToken(token);

        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

    private bool ValidateToken(string token)
    
    {
       

        var connectionString = "Data Source=.\\SQL2022;Initial Catalog=Menassa;Persist Security Info=True;User ID=sa;Password=201015;"; // عدلها حسب بيئتك

        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand(@"
                SELECT count( tokenid) FROM tbl_Tokens where AccessToken = @token 
                  AND Expiration > GETDATE() 
                  AND IsRevoked = 0", conn);

            cmd.Parameters.AddWithValue("@token", token);

            int count = (int)cmd.ExecuteScalar();

            return count > 0;
        }
    }
}

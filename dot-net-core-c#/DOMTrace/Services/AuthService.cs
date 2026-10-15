using DOMTrace.Core;
using DOMTrace.Models;

namespace DOMTrace.Services;

public class AuthService
{
    private readonly MySqlContext _mySqlService;

    public AuthService()
    {
        _mySqlService = new MySqlContext(ApplicationConstants.MySqlConnection);
    }

    public ApplicationUser Login(string username, string password)
    {
        if (username != "admin" || password != "admin")
            return null;

        var sqlQuery = $"call T2.sp_sl_logon({username}, {password})";
        var result = _mySqlService.ExecuteQuery(sqlQuery);
        if (result.Rows.Count == 0)
            return null;
        // Assuming the stored procedure returns a single row with user details
        var row = result.Rows[0];
        return new ApplicationUser
        {
            UserId = Convert.ToInt32(row["UserId"]),
            UserName = row["UserName"].ToString()
        };
    }
}

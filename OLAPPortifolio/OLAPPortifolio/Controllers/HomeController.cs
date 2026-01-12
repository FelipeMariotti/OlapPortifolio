using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OLAPPortifolio.Models;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace OLAPPortifolio.Controllers;

public class HomeController : Controller
{
    private readonly IDbConnection _dbConnection;

    public HomeController(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }   

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult LoginSubmit([FromBody] string user, string password)
    {


        return Ok();
    }
    public async Task<IActionResult> RegisterUser([FromBody] string user, string email, string document, string passwrd)
    {

        await using var conn = (NpgsqlConnection)_dbConnection;
        await conn.OpenAsync();

        await using var tx = await conn.BeginTransactionAsync();

        UserRegisterDTO newUser = new UserRegisterDTO(0, user, email, passwrd, true, false, DateTime.UtcNow.AddMinutes(30).ToString(), "JWT", document, DateTime.UtcNow);

        try
        {
            var cmd = new NpgsqlCommand(@"INSERT INTO cadastro.tb (Nome, Email, Password, IsActive, IsAdmin, Expiration, JwtToken, 
                                            Document, CreatedAt) 
                                            VALUES 
                                            (@user, @email, @password, @isactive, @admin, @expiration, @jwtToken, @document @created)", 
                                            conn, tx
            );

            cmd.Parameters.AddWithValue("user", newUser.Username);
            cmd.Parameters.AddWithValue("email", newUser.Email);
            cmd.Parameters.AddWithValue("password", newUser.Password);
            cmd.Parameters.AddWithValue("isactive", true);
            cmd.Parameters.AddWithValue("admin", false);
            cmd.Parameters.AddWithValue("expiration", DateTime.UtcNow.AddMinutes(30));
            cmd.Parameters.AddWithValue("jwtToken", document);
            cmd.Parameters.AddWithValue("document", document);
            cmd.Parameters.AddWithValue("created", DateTime.UtcNow);

            await cmd.ExecuteNonQueryAsync();

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;

        }
        return Ok();
    }
}



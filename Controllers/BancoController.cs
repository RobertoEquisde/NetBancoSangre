using Microsoft.AspNetCore.Mvc;
using banco_sangre.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
namespace banco_sangre.Controllers;

//[Authorize]
public class BancoController : Controller
{
    private readonly IConfiguration _configuration;
    public BancoController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }
   

}

// Controllers/AccountController.cs

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ApiController : Controller
{
    [Authorize]
    public IActionResult HelloWorld()
    {
        return Json(new { Hello= this.User.Identity.Name });
    }
}
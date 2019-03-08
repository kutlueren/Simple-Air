using Microsoft.AspNetCore.Mvc;
using SimpleAir.API.Model;
using System.Diagnostics;

namespace SimpleAir.API.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                HttpContext.TraceIdentifier
            });
        }
    }
}
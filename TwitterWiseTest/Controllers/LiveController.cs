using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TwitterWise.Controllers
{
    public class LiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}

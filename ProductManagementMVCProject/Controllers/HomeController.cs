using Microsoft.AspNetCore.Mvc;
using ProductManagementMVCProject.Models;
using System.Diagnostics;

namespace ProductManagementMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoIdentityMVCProjectContext _demoIdentityMVCProjectContext;

        public HomeController(ILogger<HomeController> logger, DemoIdentityMVCProjectContext demoIdentityMVCProjectContext)
        {
            _logger = logger;
            _demoIdentityMVCProjectContext = demoIdentityMVCProjectContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
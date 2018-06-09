using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmotionalReddit.MVC.Models;
using EmotionalReddit.MVC.ViewModels;

namespace EmotionalReddit.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var homeVM = new HomeViewModel();
            homeVM.AddRedditItem("test story 1", string.Empty, string.Empty);
            homeVM.AddRedditItem("test story 2", string.Empty, string.Empty);
            homeVM.AddRedditItem("test story 3", string.Empty, string.Empty);
            homeVM.AddRedditItem("test story 4", string.Empty, string.Empty);

            return View(homeVM);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

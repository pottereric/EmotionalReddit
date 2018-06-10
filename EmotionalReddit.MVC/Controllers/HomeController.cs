using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmotionalReddit.MVC.Models;
using EmotionalReddit.MVC.ViewModels;
using Microsoft.Extensions.Configuration;
using EmotionalReddit.MVC.Services;

namespace EmotionalReddit.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRedditSentiment _redditSentiment;

        public HomeController(IConfiguration configuration, IRedditSentiment redditSentiment)
        {
            _configuration = configuration;
            _redditSentiment = redditSentiment;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeViewModel();
            var cogSerKey = _configuration["CogSerKey:InstrumentationKey"];

            var redditItems = _redditSentiment.GetRedditItemSentimentModels(cogSerKey);

            foreach (var f in redditItems)
            {
                homeVM.AddRedditItem(f.Title, f.Score, f.Sentiment, f.LinkUrl, f.DiscussionUrl);
            }

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

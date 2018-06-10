using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmotionalReddit.MVC.Models;
using EmotionalReddit.MVC.ViewModels;
using Microsoft.Extensions.Configuration;

namespace EmotionalReddit.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeViewModel();
            //homeVM.AddRedditItem("test story 1", string.Empty, string.Empty);
            //homeVM.AddRedditItem("test story 2", string.Empty, string.Empty);
            //homeVM.AddRedditItem("test story 3", string.Empty, string.Empty);
            //homeVM.AddRedditItem("test story 4", string.Empty, string.Empty);

            var cogSerKey = _configuration["CogSerKey:InstrumentationKey"];

            var redditTitlesWithSentiment = EmotionalReddit.RedditSentimentAnalyzer.RedditSentiment.getSentimentAndTitlesForsubreddit(
                cogSerKey, "programming", "top");

            var negativeTitles = redditTitlesWithSentiment.Where(i => i.Sentiment.Value > 0.5);

            foreach (var b in negativeTitles)
            {
                homeVM.AddRedditItem(b.Title, "", "");
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

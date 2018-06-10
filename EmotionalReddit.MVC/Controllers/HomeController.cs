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
            HomeViewModel homeVM = BuildViewModelForSubreddit("programming", 0.7);
            return View(homeVM);
        }

        private HomeViewModel BuildViewModelForSubreddit(string subredditName, double sentimentFilterLevel)
        {
            var homeVM = new HomeViewModel() { SubRedditName = subredditName, SentimentFilter = (int)sentimentFilterLevel * 10 };
            var cogSerKey = _configuration["CogSerKey:InstrumentationKey"];

            var redditItems = _redditSentiment.GetRedditItemSentimentModels(cogSerKey, subredditName, sentimentFilterLevel);

            foreach (var f in redditItems)
            {
                homeVM.AddRedditItem(f.Title, f.Score, f.Sentiment, f.LinkUrl, f.DiscussionUrl);
            }

            return homeVM;
        }

        [HttpPost]
        public IActionResult Update(HomeViewModel vm)
        {
            HomeViewModel homeVM = BuildViewModelForSubreddit(vm.SubRedditName, vm.SentimentFilterLevel);
            return View("Index", homeVM);
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

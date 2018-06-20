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
using Microsoft.ApplicationInsights;

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

        [HttpPost]
        public IActionResult Index(HomeViewModel vm)
        {
            HomeViewModel homeVM = BuildViewModelForSubreddit(vm.SubRedditName, vm.SentimentFilterLevel);
            return View(homeVM);
        }

        [HttpGet]
        [Route("/r/{subreddit}")]
        public IActionResult Index(string subreddit)
        {
            HomeViewModel homeVM = BuildViewModelForSubreddit(subreddit, 0.7);
            return View(homeVM);
        }

        [HttpPost]
        [Route("/r/{subreddit}")]
        public IActionResult Index(string subreddit, HomeViewModel vm)
        {
            string subredditName = String.IsNullOrWhiteSpace(vm.SubRedditName) ?
                                    subreddit : vm.SubRedditName;
            HomeViewModel homeVM = BuildViewModelForSubreddit(subredditName, vm.SentimentFilterLevel);
            return View(homeVM);
        }


        private HomeViewModel BuildViewModelForSubreddit(string subredditName, double sentimentFilterLevel)
        {
            try
            {
                TrackTelemetryOfRequest(subredditName, sentimentFilterLevel);

                var homeVM = new HomeViewModel() { SubRedditName = subredditName, SentimentFilter = (int)(sentimentFilterLevel * 10) };
                var cogSerKey = _configuration["CogSerKey:InstrumentationKey"];

                var redditItems = _redditSentiment.GetRedditItemSentimentModels(cogSerKey, subredditName, sentimentFilterLevel);

                foreach (var f in redditItems)
                {
                    homeVM.AddRedditItem(f.Title, f.Score, f.Sentiment, f.LinkUrl, f.DiscussionUrl);
                }

                return homeVM;
            }
            catch (Exception ex)
            {
                // TODO log error or display error
                return new HomeViewModel() { SubRedditName = subredditName, SentimentFilter = (int)(sentimentFilterLevel * 10) };
            }
        }

        private void TrackTelemetryOfRequest(string subredditName, double sentimentFilterLevel)
        {
            var telemetryData = new Dictionary<string, string>();
            telemetryData.Add("subredditName", subredditName);
            telemetryData.Add("sentimentFilterLevel", sentimentFilterLevel.ToString());
            var telemetryClient = new TelemetryClient();
            telemetryClient.InstrumentationKey = _configuration["ApplicationInsights:InstrumentationKey"]; ;
            telemetryClient.TrackEvent("GeneratingRedditView", telemetryData);
        }

       

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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

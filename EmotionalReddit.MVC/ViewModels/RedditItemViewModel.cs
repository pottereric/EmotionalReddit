using EmotionalReddit.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.ViewModels
{
    public class RedditItemViewModel
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public double Sentiment { get; set; }
        public string StoryUrl { get; set; }
        public string DiscussionUrl { get; set; }

        public RedditItemViewModel()
        {

        }

        public RedditItemViewModel(RedditItemSentimentModel model)
        {
            Title = model.Title;
            StoryUrl = model.LinkUrl;
            DiscussionUrl = model.DiscussionUrl;
            Score = model.Score;
            Sentiment = model.Sentiment;

        }
    }
}

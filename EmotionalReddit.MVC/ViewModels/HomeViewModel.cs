using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.ViewModels
{

    public class HomeViewModel
    {
        private List<RedditItemViewModel> _redditItems = new List<RedditItemViewModel>();
        public IEnumerable<RedditItemViewModel> RedditItems => _redditItems;

        public void AddRedditItem(string title, int score, double sentiment, string storyUrl, string discussionUrl)
        {
            var redditItem = new RedditItemViewModel()
            {
                Title = title,
                Score = score,
                Sentiment = sentiment,
                StoryUrl = storyUrl,
                DiscussionUrl = discussionUrl
            };

            _redditItems.Add(redditItem);
        }

        public string SubRedditName { get; set; }

        public int SentimentFilter { get; set; }

        public double SentimentFilterLevel => ((double)SentimentFilter) / 10;
    }
}

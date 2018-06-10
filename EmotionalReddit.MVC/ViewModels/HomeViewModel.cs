using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.ViewModels
{

    public class HomeViewModel
    {
        private List<RedditItemViewModel> _redditItems;
        public IEnumerable<RedditItemViewModel> RedditItems => _redditItems;

        public void AddRedditItem(string title, int score, double sentiment, string storyUrl, string discussionUrl)
        {
            if(_redditItems == null)
            {
                _redditItems = new List<RedditItemViewModel>();
            }

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
    }
}

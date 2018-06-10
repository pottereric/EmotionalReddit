using EmotionalReddit.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.Services
{
    public class RedditSentiment : IRedditSentiment
    {
        public IEnumerable<RedditItemSentimentModel> GetRedditItemSentimentModels(string cogSerKey)
        {
            var redditTitlesWithSentiment = EmotionalReddit.RedditSentimentAnalyzer.RedditSentiment.getSentimentAndTitlesForsubreddit(
                cogSerKey, "programming", "top");

            var positiveTitles = redditTitlesWithSentiment.Where(i => i.Sentiment.Value > 0.5);

            var results = positiveTitles.Select(t => new RedditItemSentimentModel() { Title = t.Title });
            return results;
                
        }
    }
}

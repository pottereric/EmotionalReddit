using EmotionalReddit.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.Services
{
    public class RedditSentiment : IRedditSentiment
    {
        public IEnumerable<RedditItemSentimentModel> GetRedditItemSentimentModels(string cogSerKey, string subredditName)
        {
            var redditTitlesWithSentiment = EmotionalReddit.RedditSentimentAnalyzer.RedditSentiment.getSentimentAndTitlesForsubreddit(
                cogSerKey, subredditName, "top");

            var positiveTitles = redditTitlesWithSentiment.Where(i => i.Sentiment.Value > 0.5);

            var results = positiveTitles.Select(t => new RedditItemSentimentModel() { Title = t.Title, Score = t.Votes, Sentiment = t.Sentiment.Value });
            return results;
                
        }
    }
}

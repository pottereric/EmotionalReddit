using EmotionalReddit.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.Services
{
    public class RedditSentiment : IRedditSentiment
    {
        public IEnumerable<RedditItemSentimentModel> GetRedditItemSentimentModels(string cogSerKey, string subredditName, double sentimentFilterLevel)
        {
            var redditTitlesWithSentiment = EmotionalReddit.RedditSentimentAnalyzer.RedditSentiment.getSentimentAndTitlesForsubreddit(
                cogSerKey, subredditName, "top");

            IEnumerable<RedditSentimentAnalyzer.RedditSentiment.RedditTitleSentiment> filteredTitles;

            if(sentimentFilterLevel < .5)
            {
                filteredTitles = redditTitlesWithSentiment.Where(i => i.Sentiment.Value < sentimentFilterLevel);
            }
            else if(sentimentFilterLevel > .5)
            {
                filteredTitles = redditTitlesWithSentiment.Where(i => i.Sentiment.Value > sentimentFilterLevel);
            }
            else
            {
                filteredTitles = redditTitlesWithSentiment;
            }
            
            

            var results = filteredTitles.Select(t => new RedditItemSentimentModel() {
                Title = t.Title, Score = t.Votes, Sentiment = t.Sentiment.Value, LinkUrl = t.LinkUrl, DiscussionUrl = t.DiscussionUrl });
            return results;
                
        }
    }
}

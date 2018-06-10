using System.Collections.Generic;
using EmotionalReddit.MVC.Models;

namespace EmotionalReddit.MVC.Services
{
    public interface IRedditSentiment
    {
        IEnumerable<RedditItemSentimentModel> GetRedditItemSentimentModels(string cogSerKey, string subredditName);
    }
}
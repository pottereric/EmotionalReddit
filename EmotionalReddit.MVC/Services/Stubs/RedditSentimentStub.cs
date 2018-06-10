using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmotionalReddit.MVC.Models;

namespace EmotionalReddit.MVC.Services.Stubs
{
    public class RedditSentimentStub : IRedditSentiment
    {
        public IEnumerable<RedditItemSentimentModel> GetRedditItemSentimentModels(string cogSerKey, string subredditName)
        {
            return new List<RedditItemSentimentModel>(){
                new RedditItemSentimentModel() { Title = "Stub Title 1", Score = 100, Sentiment = 0.1, LinkUrl = "https://www.reddit.com/r/programming/", DiscussionUrl = "https://www.reddit.com/r/programming/comments/8pt6qe/the_hardest_program_ive_ever_written/"},
                new RedditItemSentimentModel() { Title = "Stub Title 2", Score = 200, Sentiment = 0.2, LinkUrl = "https://www.reddit.com/r/programming/", DiscussionUrl = "https://www.reddit.com/r/programming/comments/8pt6qe/the_hardest_program_ive_ever_written/"},
                new RedditItemSentimentModel() { Title = "Stub Title 3", Score = 300, Sentiment = 0.3, LinkUrl = "https://www.reddit.com/r/programming/", DiscussionUrl = "https://www.reddit.com/r/programming/comments/8pt6qe/the_hardest_program_ive_ever_written/"},
                new RedditItemSentimentModel() { Title = "Stub Title 4", Score = 400, Sentiment = 0.4, LinkUrl = "https://www.reddit.com/r/programming/", DiscussionUrl = "https://www.reddit.com/r/programming/comments/8pt6qe/the_hardest_program_ive_ever_written/"},
            };
        }
    }
}

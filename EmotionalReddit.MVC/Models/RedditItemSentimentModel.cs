using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalReddit.MVC.Models
{
    public class RedditItemSentimentModel
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public double Sentiment { get; set; }
        public string LinkUrl { get; set; }
        public string DiscussionUrl { get; set; }
    }
}

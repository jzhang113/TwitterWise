using System;

namespace TwitterWise.Models
{
    public class TweetModel
    {
        public bool Status { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string Author { get; set; }
    }
}
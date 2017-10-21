using System;
using System.Collections.Generic;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TwitterWise.Models
{
    public class SearchModel
    {
        private static string consumerKey = "UO02uWmf4M8AKVWBxZgIgStlr";
        private static string consumerSecret = "JT6B1q4Ym36xIIbYn08NywDybtealgn5V6PQ35TJz2Rs1x9qZ3";
        private static long minId = long.MaxValue;
        private static string prevQuery = "";

        static SearchModel()
        {
            // set the application credentials
            ITwitterCredentials appcreds = Auth.SetApplicationOnlyCredentials(consumerKey, consumerSecret, true);
        }

        public static IEnumerable<TweetModel> Search(string query)
        {
            var searchParameters = new SearchTweetsParameters(query)
            {
                MaximumNumberOfResults = 100
            };
            minId = long.MaxValue;
            prevQuery = query;

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            AddTweets(match, results);

            return results;
        }

        public static IEnumerable<TweetModel> SearchMore()
        {
            var searchParameters = new SearchTweetsParameters(prevQuery)
            {
                MaximumNumberOfResults = 100,
                MaxId = minId
            };

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            AddTweets(match, results);

            return results;
        }

        private static void AddTweets(IEnumerable<ITweet> match, ICollection<TweetModel> list)
        {
            foreach (var tweet in match)
            {
                if (!tweet.IsRetweet)
                {
                    minId = Math.Min(minId, tweet.Id);

                    list.Add(new TweetModel()
                    {
                        Text = tweet.Text,
                        Time = tweet.CreatedAt,
                        Author = tweet.CreatedBy.Name
                    });

                    AdviceModel.ProcessTweet(tweet);
                }
            }
        }
    }
}
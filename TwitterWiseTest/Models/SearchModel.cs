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

        public static ICollection<TweetModel>[] Search(string query)
        {
            var searchParameters = new SearchTweetsParameters(query)
            {
                MaximumNumberOfResults = 100
            };
            minId = long.MaxValue;
            prevQuery = query;

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            ICollection<TweetModel> filteredResults = new List<TweetModel>();
            AddTweets(match, results, filteredResults);

            ICollection<TweetModel>[] response = { results, filteredResults};
            return response;
        }

        public static ICollection<TweetModel>[] SearchMore()
        {
            var searchParameters = new SearchTweetsParameters(prevQuery)
            {
                MaximumNumberOfResults = 100,
                MaxId = minId
            };

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            ICollection<TweetModel> filteredResults = new List<TweetModel>();
            AddTweets(match, results, filteredResults);

            ICollection<TweetModel>[] response = { results, filteredResults };
            return response;
        }

        private static void AddTweets(IEnumerable<ITweet> match, ICollection<TweetModel> list, ICollection<TweetModel> filteredList)
        {
            foreach (var tweet in match)
            {
                minId = Math.Min(minId, tweet.Id);

                list.Add(new TweetModel()
                {
                    Status = true,
                    Text = tweet.Text,
                    Time = tweet.CreatedAt,
                    Author = tweet.CreatedBy.Name
                });

                if (!tweet.IsRetweet && AdviceModel.ProcessTweet(tweet))
                {
                    filteredList.Add(new TweetModel()
                    {
                        Status = true,
                        Text = tweet.Text,
                        Time = tweet.CreatedAt,
                        Author = tweet.CreatedBy.Name
                    });
                }
            }
        }
    }
}
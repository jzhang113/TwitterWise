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
        private static int pages = 0;

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
            pages = 0;
            prevQuery = query;

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            ICollection<TweetModel> filteredResults = new List<TweetModel>();
            AddTweets(match, results, filteredResults);

            ICollection<TweetModel>[] response = new ICollection<TweetModel>[2];
            response[0] = results;
            response[1] = filteredResults;
            return response;
        }

        public static ICollection<TweetModel>[] SearchMore()
        {
            if (minId < 100 || pages++ > 10)
                return null;

            var searchParameters = new SearchTweetsParameters(prevQuery)
            {
                MaximumNumberOfResults = 100,
                MaxId = minId
            };

            IEnumerable<ITweet> match = Tweetinvi.Search.SearchTweets(searchParameters);
            ICollection<TweetModel> results = new List<TweetModel>();
            ICollection<TweetModel> filteredResults = new List<TweetModel>();
            AddTweets(match, results, filteredResults);

            ICollection<TweetModel>[] response = new ICollection<TweetModel>[2];
            response[0] = results;
            response[1] = filteredResults;
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
using System.Threading;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using CircularBuffer;

namespace TwitterWise.Models
{
    public class StreamModel
    {
        private static string consumerKey = "UO02uWmf4M8AKVWBxZgIgStlr";
        private static string consumerSecret = "JT6B1q4Ym36xIIbYn08NywDybtealgn5V6PQ35TJz2Rs1x9qZ3";
        private static string userKey = "921569931216457728-TQ39ids3yPhsO37KG5id5lDVOcjSDt9";
        private static string userSecret = "bldA98fhMdMToOMITVcdU3F3IPv22BpOq1tzX72YFTct9";

        private static IFilteredStream stream;
        private static ISampleStream sampleStream;
        private static Thread thread;

        private static bool streaming = false;
        private const int CAPACITY = 100;
        private static CircularBuffer<TweetModel> recentTweets;
        

        static StreamModel()
        {
            // set the application credentials
            ITwitterCredentials appcreds = Auth.SetUserCredentials(consumerKey, consumerSecret, userKey, userSecret);
            recentTweets = new CircularBuffer<TweetModel>(CAPACITY);

            stream = Stream.CreateFilteredStream();
            stream.AddTweetLanguageFilter(LanguageFilter.English);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                AddTweet(args.Tweet);
                AdviceModel.ProcessTweet(args.Tweet);
            };

            sampleStream = Stream.CreateSampleStream();
            sampleStream.AddTweetLanguageFilter(LanguageFilter.English);
            sampleStream.TweetReceived += (sender, args) =>
            {
                AddTweet(args.Tweet);
                AdviceModel.ProcessTweet(args.Tweet);
            };
        }

        public static void Start()
        {
            if (streaming == true)
                return;

            streaming = true;

            if (stream.TracksCount == 0)
                thread = new Thread(() => sampleStream.StartStream());
            else
                thread = new Thread(() => stream.StartStreamMatchingAnyCondition());

            thread.Start();
        }

        public static void StopAll()
        {
            if (streaming == false)
                return;

            streaming = false;

            if (sampleStream.StreamState == StreamState.Running)
                sampleStream.StopStream();

            if (stream.StreamState == StreamState.Running)
                stream.StopStream();
        }

		public static void AddWatch(string query)
        {
            if (sampleStream.StreamState == StreamState.Running)
                sampleStream.StopStream();

            stream.StopStream();
            stream.AddTrack(query);

            streaming = true;
            thread = new Thread(() => stream.StartStreamMatchingAnyCondition());
            thread.Start();
        }

        public static void RemoveWatch(string query)
        {
            if (sampleStream.StreamState == StreamState.Running)
                sampleStream.StopStream();

            stream.StopStream();
            stream.RemoveTrack(query);

            if (stream.TracksCount > 0 && streaming)
            {
                thread = new Thread(() => stream.StartStreamMatchingAnyCondition());
                thread.Start();
            }
            else
            {
                streaming = false;
            }
        }

        public static TweetModel GetTweet()
        {
            return recentTweets.Front();
        }

        public static CircularBuffer<TweetModel> GetAllTweets()
        {
            return recentTweets;
        }

        private static void AddTweet(ITweet tweet)
        {
            recentTweets.PushFront(new TweetModel
            {
                Text = tweet.Text,
                Time = tweet.CreatedAt,
                Author = tweet.CreatedBy.Name
            });
        }
    }
}
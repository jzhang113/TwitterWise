using System;
using System.Collections.Generic;
using System.IO;
using Tweetinvi.Models;

namespace TwitterWise.Models
{
    public class AdviceModel
    {
        private static ICollection<string> adviceKeywords = new List<string>();
        private static ICollection<string> advicePrefixes = new List<string>();

        static AdviceModel()
        {
            using (StreamReader sr = new StreamReader("adviceWords.txt"))
            {
                adviceKeywords.Add(sr.ReadLine());
            }

            using (StreamReader sr = new StreamReader("advicePrefix.txt"))
            {
                advicePrefixes.Add(sr.ReadLine());
            }
        }

        public static bool ProcessTweet(ITweet tweet)
        {
            if (tweet.IsRetweet)
                return false;

            string message = tweet.Text.ToLower();

            foreach (string word in advicePrefixes)
            {
                if (message.StartsWith(word))
                {
                    HandleTweet(tweet);
                    return true;
                }
            }

            foreach (string word in adviceKeywords)
            {
                if (message.Contains(word))
                {
                    HandleTweet(tweet);
                    return true;
                }
            }

            return false;
        }

        private static void HandleTweet(ITweet tweet)
        {
            Console.WriteLine("{0}, {1}", tweet.Id, tweet.Text);

            using (StreamWriter sw = new StreamWriter("twitteradvice.txt", true))
            {
                sw.WriteLine(tweet.Text + " - " + tweet.CreatedBy.Name);
            }
        }
    }
}

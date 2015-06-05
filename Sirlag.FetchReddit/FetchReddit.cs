using System;
using System.Collections.Generic;
using System.Linq;
using RedditSharp;
using RedditSharp.Things;

namespace Sirlag.FetchReddit
{
    class FetchReddit
    {
        private static void Main(string[] args)
        {
            var reddit = new Reddit();
            var subreddit = reddit.GetSubreddit("/r/dailyprogrammer");
            var posts = new List<Post>();
            var i = 0;
            Console.WriteLine("Current Exercises: ");
            foreach (var post in subreddit.New.Take(3).Where(post => !post.Title.StartsWith("[Weekly")))
            {
                i++;
                posts.Add(post);
                Console.WriteLine("\t{0} - {1}", i, post.Title);
            }
            Console.Write("View");
            var answer = Console.ReadLine();
            PrintPost(posts[Convert.ToInt32(answer) - 1]);
        }

        static ConsoleColor GetConsoleColor(String line)
        {
            if (line.StartsWith("###"))
                return ConsoleColor.DarkRed;
            if (line.StartsWith("##"))
                return ConsoleColor.Cyan;
            if (line.StartsWith("#"))
                return ConsoleColor.Blue;
            return ConsoleColor.White;
        }

        private static void PrintPost(Post post)
        {
            foreach (var line in post.SelfText.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                Console.ForegroundColor = GetConsoleColor(line);
                Console.WriteLine(line);
            }
            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Leaderboard : Response
    {
        public List<LeaderboardItem> Board { get; private set; }

        public Leaderboard(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Board = new List<LeaderboardItem>();
            if (MetaCode.Equals("200"))
                foreach (
                    var obj in (Object[]) Helpers.ExtractDictionary(jsonDictionary, "response:leaderboard")["items"])
                    Board.Add(new LeaderboardItem((Dictionary<string, object>) obj));
        }
    }
}

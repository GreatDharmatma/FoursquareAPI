using System;
using System.Collections.Generic;

namespace Brahmastra.FoursquareApi.Entities
{
    public class LeaderboardItem
    {
        internal string Json = "";

        public Score ScoreThisWeek { get; private set; }
        public User LeaderBoardUser { get; private set; }
        public Int32 RankThisWeek { get; private set; }

        public LeaderboardItem(Dictionary<string, object> jsonDictionary)
        {
            RankThisWeek = Int32.Parse(jsonDictionary["rank"].ToString());
            LeaderBoardUser = new User((Dictionary<string, object>)jsonDictionary["user"]);
            ScoreThisWeek = new Score((Dictionary<string, object>)jsonDictionary["scores"]);
        }
    }
}
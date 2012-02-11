using System;
using System.Collections.Generic;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class LeaderboardItem
    {
        public int Rank;
        public User User;
        public Score Score;

        public LeaderboardItem(Dictionary<string, object> JSONDictionary)
        {
            Rank = Int32.Parse(JSONDictionary["rank"].ToString());
            User = new User((Dictionary<string, object>)JSONDictionary["user"]);
            Score = new Score((Dictionary<string, object>)JSONDictionary["scores"]);
        }
    }
}
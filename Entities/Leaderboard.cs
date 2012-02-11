using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Leaderboard : Response
    {
        public List<LeaderboardItem> leaderboard = new List<LeaderboardItem>();

        public Leaderboard(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            if (metaCode.Equals("200"))
            {
                foreach (Object Obj in (Object[])Helpers.extractDictionary(JSONDictionary, "response:leaderboard")["items"])
                {
                    leaderboard.Add(new LeaderboardItem((Dictionary<string, object>)Obj));
                }
            }
        }
    }
}

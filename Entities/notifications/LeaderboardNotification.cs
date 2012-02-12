using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class LeaderboardNotification
    {
        public List<LeaderboardItem> Leaderboard { get; private set; }
        public string Message { get; private set; }
        public List<ScoreNotification> Scores { get; private set; }
        public int Total { get; private set; }

        public LeaderboardNotification(Dictionary<string,object> jsonDictionary )
        {
            //
        }
    }
} 
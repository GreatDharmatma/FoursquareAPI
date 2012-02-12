using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class LeaderboardNotification
    {
        public List<LeaderboardItem> Leaderboard { get; private set; }
        public string Message { get; private set; }
        public Notification<ScoreNotification> Scores { get; private set; }
        public int Total { get; private set; }

        public LeaderboardNotification(Dictionary<string,object> jsonDictionary )
        {
            Message = Helpers.GetDictionaryValue(jsonDictionary, "message");
            Total = int.Parse(Helpers.GetDictionaryValue(jsonDictionary, "total"));

            Scores = new Notification<ScoreNotification>(new ScoreNotification(jsonDictionary), "scores");
            Leaderboard = new List<LeaderboardItem>();

            foreach (object obj in (object[]) jsonDictionary["leaderboard"])
            {
                var v = (Dictionary<string, object>)obj;
                Leaderboard.Add(new LeaderboardItem(v));
            }
        }
    }
} 
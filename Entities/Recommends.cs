using System.Collections.Generic;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Recommends
    {
        public List<Tip> Tips { get; set; }
        public Venue Venue { get; set; }
        public List<Reason> Reasons { get; set; }

        public Recommends()
        {
            Tips = new List<Tip>();
            Reasons = new List<Reason>();
        }
    }
}

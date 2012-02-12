using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brahmastra.FoursquareAPI.Entities
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

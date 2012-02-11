using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Recommends
    {
        public List<Reason> reasons;
        public Venue venue;
        public List<Tip> tips;
    }
}

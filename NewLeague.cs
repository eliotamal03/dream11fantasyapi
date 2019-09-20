using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class NewLeague
    {
        public newsites site { get; set; }

        public class newsites
        {
            public string slug { get; set; }
            public string name { get; set; }
            public IList<newtours> tours { get; set; }
            public Matches matches { get; set; }
        }

        public class newtours
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Matches
        {
            public IList<Edge> edges { get; set; }
        }
    }

}

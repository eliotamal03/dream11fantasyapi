using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.League;

namespace CoreLibrary.Models.Response
{
    public class Edge
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public string status { get; set; }
        public IList<Squad> squads { get; set; }
        public Tour tour { get; set; }
    }
}

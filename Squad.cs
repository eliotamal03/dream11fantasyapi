using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.League;

namespace CoreLibrary.Models.Response
{
    public class Squad
    {
        public int tourId { get; set; }

        public int id { get; set; }
        public DateTime startTime { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public IList<Flag> flag { get; set; }
        public IList<FlagWithName> flagWithName { get; set; }
        public string flagNameOne { get; set; }
        public string flagNameWithNameOne { get; set; }
        public string flagNameTwo { get; set; }
        public string flagNameWithNameTwo { get; set; }
        public string jerseyColor { get; set; }

        public string TeamNameOne { get; set; }
        public string TeamNameTwo { get; set; }
        public string FullTeamName { get; set; }
        public string startingTime { get; set; }
        public string startDate { get; set; }
        public List<MatchHistory> matchHistoryLst { get; set; }
    }
}

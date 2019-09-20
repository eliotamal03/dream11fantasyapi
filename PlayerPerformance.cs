using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class PlayerPerformance
    {
        public class Artwork
        {
            public string src { get; set; }
        }

        public class Match
        {
            public string name { get; set; }
            public DateTime startTime { get; set; }
        }

        public class Performance
        {
            public Match match { get; set; }
            public double? points { get; set; }
            public double? selectionRate { get; set; }
            public string MatchName { get; set; }
            public DateTime Dateplayed { get; set; }
            public string PlayerName { get; set; }
            public string DateplayedSort { get; set; }
        }

        public class Stats
        {
            public DateTime? dob { get; set; }
            public IList<Performance> performance { get; set; }
        }

        public class Type
        {
            public string name { get; set; }
        }

        public class Squad
        {
            public string name { get; set; }
        }

        public class Player
        {
            public IList<Artwork> artwork { get; set; }
            public Stats stats { get; set; }
            public double? credits { get; set; }
            public double? points { get; set; }
            public string name { get; set; }
            public string specialization { get; set; }
            public Type type { get; set; }
            public Squad squad { get; set; }
            public string playerProfileDisplay { get; set; }
            public double currentPercentage { get; set; }
            public int PlayerId { get; set; }
        }
        public Player player { get; set; }
    }
}

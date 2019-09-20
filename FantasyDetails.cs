using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class FantasyDetails
    {
        public class FantasyScoreCardHeader
        {
            public string name { get; set; }
        }

        public class Flag
        {
            public string src { get; set; }
        }

        public class Squad
        {
            public string name { get; set; }
            public string shortName { get; set; }
            public List<Flag> flag { get; set; }
        }

        public class Player2
        {
            public string name { get; set; }
        }

        public class FantasyPoint
        {
            public double score { get; set; }
        }

        public class Player
        {
            public Player2 player { get; set; }
            public List<FantasyPoint> fantasyPoints { get; set; }
        }

        public class FantasyScoreCard
        {
            public List<Player> players { get; set; }
        }

        public class Match
        {
            public string status { get; set; }
            public List<Squad> squads { get; set; }
            public FantasyScoreCard fantasyScoreCard { get; set; }
        }

        public class Tour
        {
            public Match match { get; set; }
        }

        public class Site
        {
            public IList<FantasyScoreCardHeader> fantasyScoreCardHeader { get; set; }
            public Tour tour { get; set; }
        }

        public Site site { get; set; }
    }
}

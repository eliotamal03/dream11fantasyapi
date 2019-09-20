using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.League;

namespace CoreLibrary.Models.Response
{
    public class PlayerDetails
    {
        public class TeamPreviewArtwork
        {
            public string src { get; set; }
        }

        public class TeamCriteria
        {
            public int totalCredits { get; set; }
            public int maxPlayerPerSquad { get; set; }
            public int totalPlayerCount { get; set; }
        }

        public class Artwork
        {
            public string src { get; set; }
        }

        public class Role
        {
            public int id { get; set; }
            public IList<Artwork> artwork { get; set; }
            public string color { get; set; }
            public string name { get; set; }
            public double pointMultiplier { get; set; }
            public string shortName { get; set; }
        }

        public class PlayerType
        {
            public int id { get; set; }
            public string name { get; set; }
            public int minPerTeam { get; set; }
            public int maxPerTeam { get; set; }
            public string shortName { get; set; }
            public IList<Artwork> artwork { get; set; }
        }

        public class Flag
        {
            public string src { get; set; }
        }

        public class Squad
        {
            public int id { get; set; }
            public string name { get; set; }
            public string jerseyColor { get; set; }
            public string shortName { get; set; }
            public IList<Flag> flag { get; set; }
        }

        public class Type
        {
            public int id { get; set; }
            public int maxPerTeam { get; set; }
            public int minPerTeam { get; set; }
            public string name { get; set; }
            public string shortName { get; set; }
        }

        public class Player
        {
            public IList<Artwork> artwork { get; set; }
            public Squad squad { get; set; }
            public double credits { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public double points { get; set; }
            public Type type { get; set; }
            public bool isSelected { get; set; }
            public object role { get; set; }
        }

        public class Match
        {
            public int id { get; set; }
            public string guru { get; set; }
            public IList<Squad> squads { get; set; }
            public DateTime startTime { get; set; }
            public string status { get; set; }
            public IList<Player> players { get; set; }
        }

        public class Tour
        {
            public Match match { get; set; }
        }

        public class Site
        {
            public string name { get; set; }
            public IList<TeamPreviewArtwork> teamPreviewArtwork { get; set; }
            public TeamCriteria teamCriteria { get; set; }
            public IList<Role> roles { get; set; }
            public IList<PlayerType> playerTypes { get; set; }
            public Tour tour { get; set; }
        }

        public class Me
        {
            public bool isGuestUser { get; set; }
        }

        public Site site { get; set; }
        public Me me { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.PlayerDetails;

namespace CoreLibrary.Models.Response
{
    public class League
    {
        public class Me
        {
            public bool isGuestUser { get; set; }
            public bool showOnboarding { get; set; }
        }

        public class Flag
        {
            public string src { get; set; }
            public object type { get; set; }
        }

        public class FlagWithName
        {
            public string src { get; set; }
            public object type { get; set; }
        }

        public class Tour
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class Matches
        {
            public IList<Edge> edges { get; set; }
        }

        public class Site
        {
            public string slug { get; set; }
            public string name { get; set; }
            public IList<Tour> tours { get; set; }
            public Matches matches { get; set; }
            public bool canCreateContest { get; set; }
            public bool showWalletIcon { get; set; }
            public IList<TeamPreviewArtwork> teamPreviewArtwork { get; set; }
            public TeamCriteria teamCriteria { get; set; }
            public IList<Role> roles { get; set; }
            public IList<PlayerType> playerTypes { get; set; }
            public Tour tour { get; set; }
        }

        public Me me { get; set; }
        public IList<Site> sites { get; set; }

    }
}

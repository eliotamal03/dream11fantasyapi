using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class Contest
    {
        public class Me
        {
            public bool isGuestUser { get; set; }
            public bool showOnboarding { get; set; }
        }

        public class Site
        {
            public bool canCreateContest { get; set; }
            public string name { get; set; }
            public bool showWalletIcon { get; set; }
        }

        public class Artwork
        {
            public string src { get; set; }
        }

        public class EntryFee
        {
            public int amount { get; set; }
            public string symbol { get; set; }
        }

        public class PrizeAmount
        {
            public int amount { get; set; }
            public string symbol { get; set; }
        }

        public class Match
        {
            public int id { get; set; }
            public string status { get; set; }
        }

        public class Tour
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class DisplayContest
        {
            public string contestCategory { get; set; }
            public int contestSize { get; set; }
            public int currentSize { get; set; }
            public EntryFee entryFee { get; set; }
            public bool hasJoined { get; set; }
            public int id { get; set; }
            public string inviteCode { get; set; }
            public bool isInfiniteEntry { get; set; }
            public bool isGuaranteed { get; set; }
            public bool isMultipleEntry { get; set; }
            public bool isRecommended { get; set; }
            public int numberOfWinners { get; set; }
            public PrizeAmount prizeAmount { get; set; }
            public bool showInvite { get; set; }
            public bool isFreeEntry { get; set; }
            public Match match { get; set; }
            public Tour tour { get; set; }
            public string site { get; set; }
        }

        public class ContestSection
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public List<Artwork> artwork { get; set; }
            public int totalContestCount { get; set; }
            public List<DisplayContest> displayContests { get; set; }
        }

        public Me me { get; set; }
        public Site site { get; set; }
        public List<ContestSection> contestSections { get; set; }
    }
}

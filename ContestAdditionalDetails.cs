using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.PlayerDetails;

namespace CoreLibrary.Models.ServiceResponses
{
    public class ContestAdditionalDetails
    {
        public int EntryFee { get; set; }
        public string InviteCode { get; set; }
        public bool IsGuaranteed { get; set; }
        public bool IsMultipleEntry { get; set; }
        public int TotalWinners { get; set; }
        public int PrizeAmount { get; set; }
        public List<Player> PlayerName { get; set; }
        public List<int> WinnersList { get; set; }

        public string contestCategory { get; set; }
        public int contestSize { get; set; }
        public int currentSize { get; set; }
        public bool hasJoined { get; set; }
        public int id { get; set; }
        public bool isInfiniteEntry { get; set; }
        public int numberOfWinners { get; set; }
        public bool showInvite { get; set; }
        public bool isFreeEntry { get; set; }
        public string site { get; set; }
    }
}

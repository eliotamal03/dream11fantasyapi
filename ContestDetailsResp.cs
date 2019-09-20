using System;
using System.Collections.Generic;
using System.Text;
using static CoreLibrary.Models.Response.PlayerDetails;

namespace CoreLibrary.Models.ServiceResponses
{
    public class ContestDetailsResp
    {
        public int TotalContestCount { get; set; }
        public string ContestImg { get; set; }
        public string ContestName { get; set; }
        public string ContestDesc { get; set; }
        public int ContestId { get; set; }
        public string contestCategory { get; set; }
        public int contestSize { get; set; }
        public int currentSize { get; set; }
        public int EntryFee { get; set; }
        public string InviteCode { get; set; }
        public int numberOfWinners { get; set; }
        public int PrizeAmount { get; set; }
        public List<ContestAdditionalDetails> AddonList { get; set; }
    }
}

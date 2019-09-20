using CoreLibrary.Models.ServiceResponses;
using System.Collections.Generic;

namespace D11Analytics.ViewModels
{
    public class ContestDetails : MatchDetails
    {
        public List<ContestDetailsResp> ContestList { get; set; }

        public List<ContestAdditionalDetails> ContestAdditionalLst { get; set; }

        public List<int> WinnersList { get; set; }
    }
}

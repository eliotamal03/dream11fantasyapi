using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.ServiceResponses
{
    public class FantasyScoreDetails
    {
        public FantasyDetails.FantasyScoreCard FantasyScoreCard { get; set; }
        public IList<FantasyDetails.FantasyScoreCardHeader> FantasyHeaderLst { get; set; }
    }
}

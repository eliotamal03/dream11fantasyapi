using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D11Analytics.ViewModels
{
    public class PlayerStatsDetail
    {
        public List<PlayerPerformance.Performance> PlayerPerformanceList { get; set; }
        public List<string> playerNameLst { get; set; }
    }
}

using CoreLibrary.Models.Response;
using CoreLibrary.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D11Analytics.ViewModels
{
    public class MatchDetails
    {
        public string startingTime { get; set; }

        public List<Squad> SquadList { get; set; }

        public string flagone { get; set; }
        public string flagtwo { get; set; }
        public string flagNameOne { get; set; }
        public string flagNameTwo { get; set; }
        public string artwork { get; set; }
        public string SportName { get; set; }

        public List<PlayerPerformance.Player> PlayerList { get; set; }

        public List<PlayPercentage> PlayPercentageLst { get; set; }

        public List<MatchHistory> FantasyMatchLst { get; set; }

        public FantasyScoreDetails FantasyDetailsLst { get; set; }


    }
}

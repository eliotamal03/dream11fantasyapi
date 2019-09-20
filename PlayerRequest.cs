using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Request
{
    public class PlayerRequest
    {

        public int TourId { get; set; }
        public int MatchId { get; set; }
        public string flagNameOne { get; set; }
        public string flagNameWithNameOne { get; set; }
        public string flagNameTwo { get; set; }
        public string flagNameWithNameTwo { get; set; }
        public List<int> PlayerIdLst { get; set; }
        public string PlayerJson { get; set; }
        public string TeamNameOne { get; set; }
        public string TeamNameTwo { get; set; }
        public string FullTeamName { get; set; }
        public string StartingTime { get; set; }
        public int PlayerId { get; set; }
        public string SportName { get; set; }
    }
}

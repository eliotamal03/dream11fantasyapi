using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class MatchHistory
    {
        public string startdate;

        public string name { get; set; }
        public int tourId { get; set; }
        public int matchId { get; set; }
        public string site { get; set; }
    }
}

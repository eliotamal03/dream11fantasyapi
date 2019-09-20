using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Request
{
    public class PlayerJsonRequest
    {
        public class Variables
        {
            public int id { get; set; }
            public int tourId { get; set; }
            public int matchId { get; set; }
            public object teamId { get; set; }
            public string site { get; set; }
        }

        public string returnType { get; set; }
        public string query { get; set; }
        public Variables variables { get; set; }
    }
}

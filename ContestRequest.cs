using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Request
{
    public class ContestRequest : PlayerRequest
    {
        public int SectionId { get; set; }
        public string ContestName { get; set; }
    }
}

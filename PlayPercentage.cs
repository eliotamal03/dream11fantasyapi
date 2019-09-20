using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Models.Response
{
    public class PlayPercentage
    {
        public string firstclass { get; set; } //100-70
        public string secondclass { get; set; } //70-60
        public string thirdclass { get; set; } // 60-50
        public string fourthclass { get; set; } //50-40
        public string fifthclass { get; set; } //40-30
        public string sixthclass { get; set; } //30-20
        public string seventhclass { get; set; }//20-10
        public string eigthclass { get; set; }//10-0            
    }
}

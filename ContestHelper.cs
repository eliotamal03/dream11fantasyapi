using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Utilities
{
    public class ContestHelper
    {
        public static int GetContestSection(string contestName)
        {
            int sectionId = 0;
            switch (contestName)
            {
                case "Mega Contest":
                    sectionId = 1;
                    break;
                case "Hot Contests":
                    sectionId = 2;
                    break;
                case "Contests for Champions":
                    sectionId = 6;
                    break;
                case "More Contests":
                    sectionId = 4;
                    break;
                case "Head-to-Head":
                    sectionId = 9;
                    break;
                case "Winner Takes All":
                    sectionId = 13;
                    break;
                case "Practice Contests":
                    sectionId = 3;
                    break;

                default:
                    break;
            }
            return sectionId;
        }
    }
}

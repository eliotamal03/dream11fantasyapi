using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IServices
{
    public interface IMatchService
    {
        List<Squad> GetLeagueDetails(string sportName);
        
    }
}

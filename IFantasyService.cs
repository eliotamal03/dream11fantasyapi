using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using CoreLibrary.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IServices
{
    public interface IFantasyService
    {
        FantasyScoreDetails GetFantasyScoresList(PlayerRequest player);
    }
}

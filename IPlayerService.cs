using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IServices
{
    public interface IPlayerService
    {
        List<int> GetPlayerId(PlayerRequest player);
        List<PlayerPerformance.Player> GetPlayerPerformanceLst(PlayerRequest player);
        List<PlayPercentage> GetPlayerByPercentage(List<PlayerPerformance.Player> playerLst);
        List<PlayerPerformance.Performance> GetIndividualPlayerPerformanceLst(PlayerRequest player);
        List<string> GetPlayerName(PlayerRequest player);
    }
}

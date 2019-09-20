using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IRepository
{
    public interface IPlayerRepository
    {
        Dictionary<string, PlayerDetails> ConstructJsonForPlayer(string json);
        Dictionary<string,PlayerPerformance> ConstructJsonForPlayerPerformance(string json);
    }
}

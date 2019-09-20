using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IRepository
{
    public interface IMatchRepository
    {
        Dictionary<string, League> ConstructJsonForLeague(string json);

        Dictionary<string, NewLeague> ConstructJsonForNewLeague(string json);

    }
}

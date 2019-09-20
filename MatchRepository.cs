using CoreLibrary.IRepository;
using CoreLibrary.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class MatchRepository : IMatchRepository
    {
        public Dictionary<string, League> ConstructJsonForLeague(string json)
        {
            Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
            Dictionary<string, League> dict = JsonConvert.DeserializeObject<Dictionary<string, League>>(jsonRespo.Result);
            return dict;
        }

        public Dictionary<string, NewLeague> ConstructJsonForNewLeague(string json)
        {
            Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
            Dictionary<string, NewLeague> dict = JsonConvert.DeserializeObject<Dictionary<string, NewLeague>>(jsonRespo.Result);
            return dict;
        }
    }
}

using CoreLibrary.IRepository;
using CoreLibrary.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class FantasyRepository : IFantasyRepository
    {

        public Dictionary<string, FantasyDetails> ConstructJsonForFantasy(string json)
        {

            Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
            Dictionary<string, FantasyDetails> dict = JsonConvert.DeserializeObject<Dictionary<string, FantasyDetails>>(jsonRespo.Result);
            return dict;
        }
    }
}

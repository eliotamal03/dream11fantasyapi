using CoreLibrary.IRepository;
using CoreLibrary.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class PlayerRepository : IPlayerRepository
    {
        public Dictionary<string, PlayerDetails> ConstructJsonForPlayer(string json)
        {
            List<int> lstPlayId = new List<int>();
            Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
            Dictionary<string, PlayerDetails> dict = JsonConvert.DeserializeObject<Dictionary<string, PlayerDetails>>(jsonRespo.Result);
            return dict;
        }

        public Dictionary<string, PlayerPerformance> ConstructJsonForPlayerPerformance(string json)
        {
            Dictionary<string, PlayerPerformance> dict = new Dictionary<string, PlayerPerformance>();
            try
            {
                Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
                dict = JsonConvert.DeserializeObject<Dictionary<string, PlayerPerformance>>(jsonRespo.Result);
            }

            catch (Exception ex)
            {

            }
            return dict;

        }

    }
}

using CoreLibrary.IRepository;
using CoreLibrary.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class ContestRepository : IContestRepository
    {
        public Dictionary<string, Contest> ConstructContestJson(string json)
        {
            Dictionary<string, Contest> dict = new Dictionary<string, Contest>();
            try
            {
                Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
                dict = JsonConvert.DeserializeObject<Dictionary<string, Contest>>(jsonRespo.Result);
            }
            catch (Exception ex)
            {

            }
            return dict;

        }

        public Dictionary<string, ContestDetails> ConstructContestDetailsJson(string json)
        {
            Dictionary<string, ContestDetails> dict = new Dictionary<string, ContestDetails>();
            try
            {
                Task<string> jsonRespo = CoreLibrary.Utilities.VendorCall.CallDream11API(json);
                dict = JsonConvert.DeserializeObject<Dictionary<string, ContestDetails>>(jsonRespo.Result);
            }
            catch (Exception ex)
            {

            }
            return dict;
        }

    }
}

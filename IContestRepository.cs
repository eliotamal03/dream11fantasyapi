using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IRepository
{
    public interface IContestRepository
    {
        Dictionary<string, Contest> ConstructContestJson(string json);
        Dictionary<string, ContestDetails> ConstructContestDetailsJson(string json);
    }
}

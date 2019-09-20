using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.IRepository
{
    public interface IFantasyRepository
    {
        Dictionary<string, FantasyDetails> ConstructJsonForFantasy(string json);
    }
}

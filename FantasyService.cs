using CoreLibrary.IRepository;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using CoreLibrary.Models.ServiceResponses;
using System;
using System.Collections.Generic;

namespace ServicesLibrary
{
    public class FantasyService : IFantasyService
    {
        private readonly IFantasyRepository _fantasyRepo;

        public FantasyService(IFantasyRepository fantasyRepo)
        {
            _fantasyRepo = fantasyRepo;
        }
        public FantasyScoreDetails GetFantasyScoresList(PlayerRequest player)
        {
            FantasyScoreDetails fantasyLst = new FantasyScoreDetails();
            string json = string.Empty;
            Dictionary<string, FantasyDetails> dict = new Dictionary<string, FantasyDetails>();
            if (player !=null && player.SportName.Equals("Kabaddi"))
            {
                json = "{\"query\":\"query FantasyScoreCard($site: String!, $tourId: Int!, $matchId: Int!) { site(slug: $site) { fantasyScoreCardHeader { name } tour(id: $tourId) { match(id: $matchId) { status squads { name shortName flag { src } } fantasyScoreCard { players { player { name } fantasyPoints { score } } } } } } }\",\"variables\":{\"tourId\":"+ player.TourId + ",\"matchId\": " + player.MatchId +",\"site\":\"" + player.SportName.ToLower() + "\"}}";
                dict = _fantasyRepo.ConstructJsonForFantasy(json);
            }
            else
            {
                json = "{\"returnType\":\"response\",\"query\":\"query FantasyScoreCard($site: String!, $tourId: Int!, $matchId: Int!) { site(slug: $site) { fantasyScoreCardHeader { name } tour(id: $tourId) { match(id: $matchId) { status squads { name shortName flag { src } } fantasyScoreCard { players { player { name } fantasyPoints { score } } } } } }}\",\"variables\":{\"tourId\":" + player.TourId + ",\"matchId\":" + player.MatchId + ",\"site\":\"" + player.SportName + "\"}}";
                dict = _fantasyRepo.ConstructJsonForFantasy(json);
            }
            IList<FantasyDetails.FantasyScoreCardHeader> headerLst = dict["data"].site.fantasyScoreCardHeader;
            FantasyDetails.FantasyScoreCard fantasyCard = dict["data"].site.tour.match.fantasyScoreCard;
            fantasyLst.FantasyScoreCard = fantasyCard;
            fantasyLst.FantasyHeaderLst = headerLst;
            return fantasyLst;
        }
    }
}

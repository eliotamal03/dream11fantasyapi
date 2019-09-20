using CoreLibrary.IRepository;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesLibrary
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerService(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }

        public List<int> GetPlayerId(PlayerRequest player)
        {
            string json = "{\"returnType\":\"response\",\"query\":\"query CreateTeamQuery( $site: String! $tourId: Int! $teamId: Int = -1 $matchId: Int!) { site(slug: $site) { name teamPreviewArtwork { src } teamCriteria { totalCredits maxPlayerPerSquad totalPlayerCount } roles { id artwork { src } color name pointMultiplier shortName } playerTypes { id name minPerTeam maxPerTeam shortName artwork { src } } tour(id: $tourId) { match(id: $matchId) { id guru squads { flag { src } id jerseyColor name shortName } startTime status players(teamId: $teamId) { artwork { src } squad { id name jerseyColor shortName } credits id name points type { id maxPerTeam minPerTeam name shortName } isSelected role { id artwork { src } color name pointMultiplier shortName } } } } } me { isGuestUser }}\",\"variables\":{\"tourId\":" + player.TourId + ",\"matchId\":" + player.MatchId + ",\"teamId\":null,\"site\":\"cricket\"}}";
            Dictionary<string, PlayerDetails> dict = _playerRepo.ConstructJsonForPlayer(json);
            List<int> playerListId = GetPlayerIdfromDict(dict);
            return playerListId;
        }

        public List<string> GetPlayerName(PlayerRequest player)
        {
            string json = "{\"returnType\":\"response\",\"query\":\"query CreateTeamQuery( $site: String! $tourId: Int! $teamId: Int = -1 $matchId: Int!) { site(slug: $site) { name teamPreviewArtwork { src } teamCriteria { totalCredits maxPlayerPerSquad totalPlayerCount } roles { id artwork { src } color name pointMultiplier shortName } playerTypes { id name minPerTeam maxPerTeam shortName artwork { src } } tour(id: $tourId) { match(id: $matchId) { id guru squads { flag { src } id jerseyColor name shortName } startTime status players(teamId: $teamId) { artwork { src } squad { id name jerseyColor shortName } credits id name points type { id maxPerTeam minPerTeam name shortName } isSelected role { id artwork { src } color name pointMultiplier shortName } } } } } me { isGuestUser }}\",\"variables\":{\"tourId\":" + player.TourId + ",\"matchId\":" + player.MatchId + ",\"teamId\":null,\"site\":\"cricket\"}}";
            Dictionary<string, PlayerDetails> dict = _playerRepo.ConstructJsonForPlayer(json);
            List<string> playerListId = GetPlayerNamefromDict(dict);
            return playerListId;
        }

        //public void GetFantasyScore(PlayerRequest player)
        //{
        //    string json = "{\"returnType\": \"response\",\"query\": \"query FantasyScoreCard($site: String!, $tourId: Int!, $matchId: Int!) { site(slug: $site) { fantasyScoreCardHeader { name } tour(id: $tourId) { match(id: $matchId) { status squads { name shortName flag { src } } fantasyScoreCard { players { player { name } fantasyPoints { score } } } } } }}\",\"variables\": {\"tourId\": " + player.TourId + ",\"matchId\":  " + player.MatchId + ",\"site\": \"cricket\"}}";
        //}

        private List<int> GetPlayerIdfromDict(Dictionary<string, PlayerDetails> dict)
        {
            List<int> lstId = new List<int>();
            IList<PlayerDetails.Player> playerLst = dict["data"].site.tour.match.players;
            foreach (var items in playerLst)
            {
                int playerId = 0;
                playerId = items.id;
                lstId.Add(playerId);
            }
            return lstId;
        }

        private List<string> GetPlayerNamefromDict(Dictionary<string, PlayerDetails> dict)
        {
            List<string> lstId = new List<string>();
            IList<PlayerDetails.Player> playerLst = dict["data"].site.tour.match.players;
            foreach (var items in playerLst)
            {
                string playerId = string.Empty;
                playerId = items.name;
                lstId.Add(playerId);
            }
            return lstId;
        }

        public List<PlayerPerformance.Player> GetPlayerPerformanceLst(PlayerRequest player)
        {

            PlayerJsonRequest vars = JsonConvert.DeserializeObject<PlayerJsonRequest>(player.PlayerJson);
            List<PlayerPerformance.Player> performLst = new List<PlayerPerformance.Player>();
            foreach (var items in player.PlayerIdLst)
            {
                vars.variables.matchId = player.MatchId;
                vars.variables.id = items;
                vars.variables.tourId = player.TourId;
                string json = JsonConvert.SerializeObject(vars);
                Dictionary<string, PlayerPerformance> performance = _playerRepo.ConstructJsonForPlayerPerformance(json);
                string[] teamArray = player.FullTeamName.Split("vs");
                string convertedTeamName = teamArray[1] + " Vs " + teamArray[0];
                PlayerPerformance.Player playerDetails = ConstructPlayerDetails(performance, convertedTeamName.Trim());
                playerDetails.PlayerId = items;
                performLst.Add(playerDetails);
            }
            return performLst;
        }

        private PlayerPerformance.Player ConstructPlayerDetails(Dictionary<string, PlayerPerformance> playerDict, string fullTeamName)
        {
            IList<PlayerPerformance.Performance> playerEnumLst = playerDict["data"].player.stats.performance;
            PlayerPerformance.Player playerPerformance = new PlayerPerformance.Player();
            playerPerformance.name = playerDict["data"].player.name;
            playerPerformance.credits = playerDict["data"].player.credits;
            playerPerformance.points = playerDict["data"].player.points;
            playerPerformance.specialization = playerDict["data"].player.type.name;
            playerPerformance.artwork = playerDict["data"].player.artwork;
            foreach (var items in playerEnumLst)
            {
                if (items.match.startTime.Date >= DateTime.Now.Date.AddDays(1) && items.match.startTime.Date <= DateTime.Now.Date.AddDays(1))
                    if (items.match.name.Equals(fullTeamName))
                    {
                        playerPerformance.currentPercentage = items.selectionRate ?? 0;
                    }
            }

            return playerPerformance;
        }

        public List<PlayPercentage> GetPlayerByPercentage(List<PlayerPerformance.Player> playerLst)
        {
            List<PlayPercentage> playerPercentageLst = new List<PlayPercentage>();
            foreach (var items in playerLst)
            {
                PlayPercentage percentPlayer = new PlayPercentage();
                if (items.currentPercentage > 0 && items.currentPercentage < 10)
                {
                    percentPlayer.eigthclass = items.name;
                }
                else if (items.currentPercentage > 10 && items.currentPercentage < 20)
                {
                    percentPlayer.seventhclass = items.name;
                }
                else if (items.currentPercentage > 20 && items.currentPercentage < 30)
                {
                    percentPlayer.sixthclass = items.name;
                }
                else if (items.currentPercentage > 30 && items.currentPercentage < 40)
                {
                    percentPlayer.fifthclass = items.name;
                }
                else if (items.currentPercentage > 40 && items.currentPercentage < 50)
                {
                    percentPlayer.fourthclass = items.name;
                }
                else if (items.currentPercentage > 50 && items.currentPercentage < 60)
                {
                    percentPlayer.thirdclass = items.name;
                }
                else if (items.currentPercentage > 60 && items.currentPercentage < 70)
                {
                    percentPlayer.secondclass = items.name;
                }
                else if (items.currentPercentage > 70 && items.currentPercentage < 100)
                {
                    percentPlayer.firstclass = items.name;
                }
                playerPercentageLst.Add(percentPlayer);
            }
            return playerPercentageLst;
        }

        public List<PlayerPerformance.Performance> GetIndividualPlayerPerformanceLst(PlayerRequest player)
        {
            PlayerJsonRequest vars = JsonConvert.DeserializeObject<PlayerJsonRequest>(player.PlayerJson);
            List<PlayerPerformance.Player> performLst = new List<PlayerPerformance.Player>();
            vars.variables.matchId = player.MatchId;
            vars.variables.id = player.PlayerId;
            vars.variables.tourId = player.TourId;
            string json = JsonConvert.SerializeObject(vars);
            Dictionary<string, PlayerPerformance> performance = _playerRepo.ConstructJsonForPlayerPerformance(json);
            List<PlayerPerformance.Performance> playerStatsLst = GetPlayerStats(performance);
            return playerStatsLst;
        }

        private List<PlayerPerformance.Performance> GetPlayerStats(Dictionary<string, PlayerPerformance> playerDict)
        {
            List<PlayerPerformance.Performance> playerStatsLst = new List<PlayerPerformance.Performance>();
            IList<PlayerPerformance.Performance> playerEnumLst = playerDict["data"].player.stats.performance;
            foreach (var items in playerEnumLst)
            {
                PlayerPerformance.Performance performance = new PlayerPerformance.Performance()
                {
                    selectionRate = items.selectionRate,
                    points = items.points,
                    MatchName = items.match.name,
                    Dateplayed = TimeZoneInfo.ConvertTimeFromUtc(items.match.startTime,
                TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")),
                    PlayerName = playerDict["data"].player.name
                };
                string date = performance.Dateplayed.ToString("dd-MM-yyyy");
                string[] dateArray = date.Split("-");
                performance.DateplayedSort = dateArray[2] + dateArray[1] + dateArray[0];
                playerStatsLst.Add(performance);
            }
            return playerStatsLst;
        }
    }
}

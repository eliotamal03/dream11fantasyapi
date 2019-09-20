using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D11Analytics.Models;
using CoreLibrary.Models.Response;
using CoreLibrary.IServices;
using D11Analytics.ViewModels;
using CoreLibrary.Utilities;
using Microsoft.AspNetCore.Http;
using CoreLibrary.Models.Request;
using StackExchange.Redis;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using CoreLibrary.Models.ServiceResponses;
using System.Net;

namespace D11Analytics.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IHostingEnvironment _env;
        private readonly IPlayerService _playerService;
        private readonly IFantasyService _fantasyService;

        public HomeController(IMatchService matchService, IPlayerService playerService, IFantasyService fantasyService, IHostingEnvironment env)
        {
            _matchService = matchService;
            _playerService = playerService;
            _fantasyService = fantasyService;
            _env = env;
        }

        public IActionResult Dashboard()
        {
            //string pdfName = "";
            //string csvName = "";
            //Task<HttpStatusCode> testData = PdfTable.PDFToExcel(pdfName,csvName);
            return View();
        }

        public IActionResult Index(string sportName)
        {
            MatchDetails matchDetails = new MatchDetails();
            try
            {
                List<Squad> leagueResponse = _matchService.GetLeagueDetails(sportName);
                string webroot = _env.WebRootPath;
                matchDetails.SquadList = leagueResponse;
                matchDetails.SportName = sportName;
                var filePath = System.IO.Path.Combine(webroot, "FantasyCard.json");
                var jsonData = System.IO.File.ReadAllText(filePath);
                List<MatchHistory> persons = JsonConvert.DeserializeObject<List<MatchHistory>>(jsonData);
                List<MatchHistory> matchLst = new List<MatchHistory>();
                if (persons == null)
                {
                    persons = new List<MatchHistory>();
                }
                foreach (var items in matchDetails.SquadList)
                {
                    MatchHistory matchHistory = new MatchHistory();
                    matchHistory.matchId = items.id;
                    matchHistory.name = items.name;
                    matchHistory.tourId = items.tourId;
                    matchHistory.site = sportName;
                    matchHistory.startdate = items.startDate;
                    persons.Add(matchHistory);
                }

                string newJson = JsonConvert.SerializeObject(persons);
                System.IO.File.WriteAllText(filePath, newJson);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string webroot = _env.WebRootPath;
                var file = System.IO.Path.Combine(webroot, "error.txt");
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(DateTime.Now + ":---> " + error);
                }
            }
            return View(matchDetails);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult OptionsJson(PlayerRequest req)
        {
            HttpContext.Session.SetString("tourId", req.TourId.ToString());
            HttpContext.Session.SetString("MatchId", req.MatchId.ToString());
            HttpContext.Session.SetString("FullTeamName", req.FullTeamName);
            HttpContext.Session.SetString("SportName", req.SportName);


            HttpContext.Session.SetString("flagNameOne", req.flagNameOne);
            HttpContext.Session.SetString("flagNameWithNameOne", req.flagNameWithNameOne);
            HttpContext.Session.SetString("TeamNameOne", req.TeamNameOne);

            HttpContext.Session.SetString("flagNameTwo", req.flagNameTwo);
            HttpContext.Session.SetString("flagNameWithNameTwo", req.flagNameWithNameTwo);
            HttpContext.Session.SetString("TeamNameTwo", req.TeamNameTwo);

            HttpContext.Session.SetString("StartingTime", req.StartingTime);
            return Ok("true");
        }

        public IActionResult Options()
        {
            MatchDetails match = new MatchDetails();
            match.flagNameOne = HttpContext.Session.GetString("flagNameWithNameOne");
            match.flagNameTwo = HttpContext.Session.GetString("flagNameWithNameTwo");
            match.startingTime = HttpContext.Session.GetString("StartingTime");
            return View(match);
        }

        public IActionResult PlayerProfile()
        {
            List<PlayerPerformance.Player> playerPerformanceLst = new List<PlayerPerformance.Player>();

            PlayerRequest req = new PlayerRequest()
            {
                MatchId = Convert.ToInt32(HttpContext.Session.GetString("MatchId")),
                TourId = Convert.ToInt32(HttpContext.Session.GetString("tourId")),
                TeamNameOne = HttpContext.Session.GetString("TeamNameOne"),
                TeamNameTwo = HttpContext.Session.GetString("TeamNameTwo"),
                FullTeamName = HttpContext.Session.GetString("FullTeamName"),
            };
            List<int> playerIdLst = _playerService.GetPlayerId(req);
            req.PlayerIdLst = playerIdLst;
            string webroot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webroot, "Player.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);
            req.PlayerJson = Json;
            playerPerformanceLst = _playerService.GetPlayerPerformanceLst(req);

            List<PlayPercentage> playerByPercent = _playerService.GetPlayerByPercentage(playerPerformanceLst);

            MatchDetails matchDetails = new MatchDetails();
            matchDetails.PlayerList = playerPerformanceLst;
            matchDetails.PlayPercentageLst = playerByPercent;
            return View(matchDetails);
        }

        public IActionResult Historical()
        {
            MatchDetails match = new MatchDetails();
            match.flagNameOne = HttpContext.Session.GetString("flagNameWithNameOne");
            match.flagNameTwo = HttpContext.Session.GetString("flagNameWithNameTwo");
            return View(match);
        }
        public IActionResult DreamTeam()
        {
            List<PlayerPerformance.Player> playerPerformanceLst = new List<PlayerPerformance.Player>();

            PlayerRequest req = new PlayerRequest()
            {
                MatchId = Convert.ToInt32(HttpContext.Session.GetString("MatchId")),
                TourId = Convert.ToInt32(HttpContext.Session.GetString("tourId")),
                TeamNameOne = HttpContext.Session.GetString("TeamNameOne"),
                TeamNameTwo = HttpContext.Session.GetString("TeamNameTwo"),
                FullTeamName = HttpContext.Session.GetString("FullTeamName"),
            };
            List<int> playerIdLst = _playerService.GetPlayerId(req);
            req.PlayerIdLst = playerIdLst;
            string webroot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webroot, "Player.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);
            req.PlayerJson = Json;
            playerPerformanceLst = _playerService.GetPlayerPerformanceLst(req);

            List<PlayPercentage> playerByPercent = _playerService.GetPlayerByPercentage(playerPerformanceLst);

            MatchDetails matchDetails = new MatchDetails();
            matchDetails.PlayerList = playerPerformanceLst;
            matchDetails.PlayPercentageLst = playerByPercent;
            return View(matchDetails);
        }

        public IActionResult FantasyMatch()
        {
            string webroot = _env.WebRootPath;
            var filePath = System.IO.Path.Combine(webroot, "FantasyCard.json");
            var jsonData = System.IO.File.ReadAllText(filePath);
            List<MatchHistory> matchHistory = JsonConvert.DeserializeObject<List<MatchHistory>>(jsonData);
            var uniqueContacts = matchHistory.AsEnumerable()
                       .GroupBy(x => x.matchId)
                       .Select(g => g.First());
            List<MatchHistory> matchHistoryFiltered = new List<MatchHistory>();
            foreach (var items in uniqueContacts)
            {
                if (items.name.Contains(HttpContext.Session.GetString("TeamNameOne")) ||
                    items.name.Contains(HttpContext.Session.GetString("TeamNameTwo")))
                {
                    matchHistoryFiltered.Add(items);
                }
            }
            MatchDetails matchDetails = new MatchDetails();
            matchDetails.FantasyMatchLst = matchHistoryFiltered;
            return View(matchDetails);
        }

        public IActionResult FantasyPoints(int tourId, int matchId, string site)
        {
            //tourId = 946;
            //matchId = 12792;
            //site = "cricket";
            PlayerRequest req = new PlayerRequest()
            {
                MatchId = matchId,
                TourId = tourId,
                SportName = site
            };
            FantasyScoreDetails fantasyLst = _fantasyService.GetFantasyScoresList(req);
            MatchDetails match = new MatchDetails();
            match.FantasyDetailsLst = fantasyLst;
            return View(match);
        }




    }
}

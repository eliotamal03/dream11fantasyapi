using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using D11Analytics.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D11Analytics.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IPlayerService _playerService;

        public AnalyticsController(IMatchService matchService, IPlayerService playerService, IHostingEnvironment env)
        {
            _playerService = playerService;
            _env = env;
        }
        // GET: Analytics
        public ActionResult PlayerStats(int playerId)
        {
            List<PlayerPerformance.Performance> playerPerformanceLst = new List<PlayerPerformance.Performance>();
            PlayerRequest req = new PlayerRequest()
            {
                MatchId = Convert.ToInt32(HttpContext.Session.GetString("MatchId")),
                TourId = Convert.ToInt32(HttpContext.Session.GetString("tourId")),
                TeamNameOne = HttpContext.Session.GetString("TeamNameOne"),
                TeamNameTwo = HttpContext.Session.GetString("TeamNameTwo"),
                FullTeamName = HttpContext.Session.GetString("FullTeamName"),
                PlayerId = playerId
            };
            List<string> playerNameLst = _playerService.GetPlayerName(req);
            string webroot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webroot, "Player.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);
            req.PlayerJson = Json;
            playerPerformanceLst = _playerService.GetIndividualPlayerPerformanceLst(req);
            PlayerStatsDetail statsDetail = new PlayerStatsDetail();
            statsDetail.PlayerPerformanceList = playerPerformanceLst;
            statsDetail.playerNameLst = playerNameLst;
            return View(statsDetail);
        }

    }
}
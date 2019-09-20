using System;
using System.Collections.Generic;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.ServiceResponses;
using D11Analytics.ViewModels;
using CoreLibrary.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D11Analytics.Controllers
{
    public class ContestController : Controller
    {
        private readonly IContestService _contestService;
        private readonly IPlayerService _playService;
        public ContestController(IContestService contestService, IPlayerService playerService)
        {
            _contestService = contestService;
            _playService = playerService;
        }

        public IActionResult Information()
        {
            ContestDetails contest = new ContestDetails();
            contest.flagNameOne = HttpContext.Session.GetString("flagNameWithNameOne");
            contest.flagNameTwo = HttpContext.Session.GetString("flagNameWithNameTwo");
            contest.startingTime = HttpContext.Session.GetString("StartingTime");
            PlayerRequest play = new PlayerRequest()
            {
                MatchId = Convert.ToInt32(HttpContext.Session.GetString("MatchId")),
                TourId = Convert.ToInt32(HttpContext.Session.GetString("tourId"))
            };
            List<ContestDetailsResp> contestLst = _contestService.GetContestDetails(play);
            contest.ContestList = contestLst;
            return View(contest);
        }


        public IActionResult ContestDetail(string contestName)
        {
            ContestDetails contest = new ContestDetails();
            contest.flagNameOne = HttpContext.Session.GetString("flagNameWithNameOne");
            contest.flagNameTwo = HttpContext.Session.GetString("flagNameWithNameTwo");
            contest.startingTime = HttpContext.Session.GetString("StartingTime");

            ContestRequest play = new ContestRequest()
            {
                MatchId = Convert.ToInt32(HttpContext.Session.GetString("MatchId")),
                TourId = Convert.ToInt32(HttpContext.Session.GetString("tourId")),
                SectionId = ContestHelper.GetContestSection(contestName)
            };
            List<ContestDetailsResp> contestLst = _contestService.GetFullContestDetails(play);
            contest.ContestAdditionalLst = contestLst[0].AddonList;
            return View(contest);
        }

        //[Route("Contest/_ContestAddon")]
        //public IActionResult _ContestAddon()
        //{
        //    return PartialView();
        //}
    }
}
using CoreLibrary.IRepository;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ServicesLibrary
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepo;
        private readonly IHostingEnvironment _env;

        public MatchService(IMatchRepository matchRepo, IHostingEnvironment env)
        {
            _matchRepo = matchRepo;
            _env = env;
        }
        public List<Squad> GetLeagueDetails(string sportName)
        {
            string kabaddiJson = "{\"returnType\":\"response\",\"query\":\"query HomeSiteMatchQuery($slug: String = null) { site(slug: $slug) { slug name tours { id name } matches(page: 0, statuses: [NOT_STARTED]) { edges { id name startTime status squads { id name shortName flag { src type } flagWithName { src type } } tour { id name slug } } } }}\",\"variables\":{\"slug\":\"kabaddi\"}}";
            string json = "{\"returnType\": \"response\",\"query\": \"query HomeQuery($slugs: [String] = null) { me { isGuestUser showOnboarding } sites(slugs: $slugs) { slug name tours { id name } matches(page: 0, statuses: [NOT_STARTED]) { edges { id name startTime status squads { id name shortName flag { src type } flagWithName { src type } } tour { id name slug } } } }}\",\"variables\": {\"slugs\":\"" + sportName + "\"}}";
            Dictionary<string, League> leagueDetails = new Dictionary<string, League>();
            Dictionary<string, NewLeague> kabaddiLeagueDetails = new Dictionary<string, NewLeague>();
            List<League.Site> lstLeague = new List<League.Site>();
            List<League.Matches> matchLst = new List<League.Matches>();
            List<MatchHistory> matchHistoryLst = new List<MatchHistory>();
            List<Squad> squadList = new List<Squad>();
            if (sportName != "Kabaddi")
            {
                leagueDetails = _matchRepo.ConstructJsonForLeague(json);
                string webroot = _env.WebRootPath;
                var file = System.IO.Path.Combine(webroot, "error.txt");
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(DateTime.Now + ":---> ");
                }
                lstLeague = leagueDetails["data"].sites.ToList();
                matchLst = GetSiteDetails(lstLeague);
                squadList = GetSquadList(matchLst);
            }
            else
            {
                kabaddiLeagueDetails = _matchRepo.ConstructJsonForNewLeague(kabaddiJson);
                NewLeague.newsites sites = kabaddiLeagueDetails["data"].site;
                squadList = GetNewSquadList(sites.matches);
            }
            return squadList;
        }

        private List<League.Matches> GetSiteDetails(List<League.Site> lstLeagueSite)
        {
            List<League.Matches> lstMatches = new List<League.Matches>();
            if (lstLeagueSite.Count > 0)
            {
                foreach (var items in lstLeagueSite)
                {
                    lstMatches.Add(items.matches);
                }
            }
            return lstMatches;
        }

        private List<Squad> GetSquadList(List<League.Matches> lstMatches)
        {
            List<Squad> squadLst = new List<Squad>();
            List<MatchHistory> matchLst = new List<MatchHistory>();
            if (lstMatches.Count > 0)
            {
                foreach (var items in lstMatches)
                {
                    foreach (var item in items.edges)
                    {
                        Squad squadDetails = new Squad();
                        squadDetails.flagNameOne = item.squads[0].flag[0].src;
                        squadDetails.flagNameWithNameOne = item.squads[0].flagWithName[0].src;
                        squadDetails.TeamNameOne = item.squads[0].shortName;

                        squadDetails.flagNameTwo = item.squads[1].flag[0].src;
                        squadDetails.flagNameWithNameTwo = item.squads[1].flagWithName[0].src;
                        squadDetails.TeamNameTwo = item.squads[1].shortName;

                        squadDetails.id = item.id;
                        squadDetails.name = item.name;
                        squadDetails.tourId = item.tour.id;
                        //squadDetails.startTime = item.startTime.ToLocalTime();
                        DateTime startDate = TimeZoneInfo.ConvertTimeFromUtc(item.startTime,
                        TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        squadDetails.startDate = startDate.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        squadDetails.startingTime = ((squadDetails.startTime.TimeOfDay - DateTime.Now.TimeOfDay).Hours.ToString()) + "hr " +
                            ((squadDetails.startTime.TimeOfDay - DateTime.Now.TimeOfDay).Minutes.ToString()) + "min";
                        squadLst.Add(squadDetails);
                    }
                    break;
                }
            }

            return squadLst;
        }

        private List<NewLeague.Matches> GetNewSiteDetails(List<NewLeague.newsites> lstLeagueSite)
        {
            List<NewLeague.Matches> lstMatches = new List<NewLeague.Matches>();
            if (lstLeagueSite.Count > 0)
            {
                foreach (var items in lstLeagueSite)
                {
                    lstMatches.Add(items.matches);
                }
            }
            return lstMatches;
        }

        private List<Squad> GetNewSquadList(NewLeague.Matches matches)
        {
            List<Squad> squadLst = new List<Squad>();
            foreach (var item in matches.edges)
            {
                Squad squadDetails = new Squad();
                squadDetails.flagNameOne = item.squads[0].flag[0].src;
                squadDetails.flagNameWithNameOne = item.squads[0].flagWithName[0].src;
                squadDetails.TeamNameOne = item.squads[0].shortName;

                squadDetails.flagNameTwo = item.squads[1].flag[0].src;
                squadDetails.flagNameWithNameTwo = item.squads[1].flagWithName[0].src;
                squadDetails.TeamNameTwo = item.squads[1].shortName;

                squadDetails.id = item.id;
                squadDetails.name = item.name;
                squadDetails.tourId = item.tour.id;
                //squadDetails.startTime = item.startTime.ToLocalTime();
                DateTime startDate = TimeZoneInfo.ConvertTimeFromUtc(item.startTime,
                TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                squadDetails.startDate = startDate.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                squadDetails.startingTime = ((squadDetails.startTime.TimeOfDay - DateTime.Now.TimeOfDay).Hours.ToString()) + "hr " +
                    ((squadDetails.startTime.TimeOfDay - DateTime.Now.TimeOfDay).Minutes.ToString()) + "min";
                squadLst.Add(squadDetails);
            }
            return squadLst;
        }

    }
}

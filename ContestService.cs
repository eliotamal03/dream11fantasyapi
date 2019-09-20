using CoreLibrary.IRepository;
using CoreLibrary.IServices;
using CoreLibrary.Models.Request;
using CoreLibrary.Models.Response;
using CoreLibrary.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesLibrary
{
    public class ContestService : IContestService
    {
        private IContestRepository _contestRepo;
        private IPlayerRepository _playerRepo;


        public ContestService(IContestRepository contestRepo, IPlayerRepository playerRepo)
        {
            _contestRepo = contestRepo;
            _playerRepo = playerRepo;
        }

        public List<ContestDetailsResp> GetContestDetails(PlayerRequest req)
        {
            List<ContestDetailsResp> contestLst = new List<ContestDetailsResp>();
            string json = "{\"returnType\": \"response\",\"query\": \"query ContestHomeData($site: String!, $tourId: Int!, $matchId: Int!) { me { isGuestUser showOnboarding } site(slug: $site) { canCreateContest name showWalletIcon } contestSections(site: $site, matchId: $matchId, tourId: $tourId) { id name description artwork { src } totalContestCount displayContests { contestCategory contestSize currentSize entryFee { amount symbol } hasJoined id inviteCode isInfiniteEntry isGuaranteed isMultipleEntry isRecommended numberOfWinners prizeAmount { amount symbol } showInvite isFreeEntry match { id status } tour { id name } site } }}\",\"variables\": {\"tourId\": 0,\"matchId\": " + req.MatchId + ",\"site\": \"cricket\",\"category\": \"PAID\"}}";
            Dictionary<string, Contest> dict = _contestRepo.ConstructContestJson(json);
            contestLst = MapContestDetailsFromDict(dict);
            return contestLst;
        }

        public List<ContestDetailsResp> GetFullContestDetails(ContestRequest req)
        {
            List<ContestDetailsResp> contestLst = new List<ContestDetailsResp>();
            string json = "{\"returnType\": \"response\",\"query\": \"query Contests( $site: String! $tourId: Int! $matchId: Int! $joiningAmount: CompareInt $category: ContestCategory $sectionIds: [Int]) { me { isGuestUser showOnboarding } site(slug: $site) { showWalletIcon maxTeamsAllowed tour(id: $tourId) { match(id: $matchId) { contests( category: $category joiningAmount: $joiningAmount contestSectionIds: $sectionIds ) { contestCategory contestSize currentSize entryFee { amount symbol } hasJoined id inviteCode isInfiniteEntry isGuaranteed isMultipleEntry numberOfWinners prizeAmount { amount symbol } showInvite isFreeEntry match { id status } tour { id name } site } name guru startTime status squads { shortName } } } }}\",\"variables\": {\"tourId\": " + req.TourId + ",\"matchId\": " + req.MatchId + ",\"site\": \"cricket\",\"sectionIds\": [" + req.SectionId + "]}}";
            Dictionary<string, ContestDetails> dict = _contestRepo.ConstructContestDetailsJson(json);
            List<ContestAdditionalDetails> addOnLst = MapFullContestDetailsFromDict(dict);
            ContestDetailsResp contest = new ContestDetailsResp();
            contest.AddonList = addOnLst;
            contestLst.Add(contest);
            return contestLst;
        }

        private List<ContestDetailsResp> MapContestDetailsFromDict(Dictionary<string, Contest> dict)
        {
            List<ContestDetailsResp> contestDetailsLst = new List<ContestDetailsResp>();
            List<Contest.ContestSection> contestList = dict["data"].contestSections;
            foreach (var items in contestList)
            {
                ContestDetailsResp contest = new ContestDetailsResp();
                contest.ContestName = items.name;
                contest.ContestDesc = items.description;
                contest.ContestId = items.id;
                contest.ContestImg = items.artwork[0].src;
                contest.TotalContestCount = items.totalContestCount;
                List<ContestAdditionalDetails> additionalContest = new List<ContestAdditionalDetails>();
                List<int> winnerLst = new List<int>();
                foreach (var additionalItem in items.displayContests)
                {
                    ContestAdditionalDetails addon = new ContestAdditionalDetails();
                    addon.WinnersList = new List<int>();
                    addon.EntryFee = additionalItem.entryFee.amount;
                    addon.InviteCode = additionalItem.inviteCode;
                    addon.PrizeAmount = additionalItem.prizeAmount.amount;
                    addon.TotalWinners = additionalItem.numberOfWinners;
                    addon.IsGuaranteed = additionalItem.isGuaranteed;
                    addon.IsMultipleEntry = additionalItem.isMultipleEntry;
                    winnerLst.Add(addon.TotalWinners);
                    addon.WinnersList = winnerLst;
                    additionalContest.Add(addon);
                }
                contest.AddonList = additionalContest;
                contestDetailsLst.Add(contest);
            }
            return contestDetailsLst;
        }

        private List<ContestAdditionalDetails> MapFullContestDetailsFromDict(Dictionary<string, ContestDetails> dict)
        {
            List<ContestAdditionalDetails> contestDetailsLst = new List<ContestAdditionalDetails>();
            List<ContestDetails.Contest> contestList = dict["data"].site.tour.match.contests;
            foreach (var items in contestList)
            {
                ContestAdditionalDetails contest = new ContestAdditionalDetails();
                contest.contestCategory = items.contestCategory;
                contest.contestSize = items.contestSize;
                contest.currentSize = items.currentSize;
                contest.EntryFee = items.entryFee.amount;
                contest.InviteCode = items.inviteCode;
                contest.numberOfWinners = items.numberOfWinners;
                contest.PrizeAmount = items.prizeAmount.amount;
                contest.IsGuaranteed = items.isGuaranteed;
                contest.IsMultipleEntry = items.isMultipleEntry;
                contestDetailsLst.Add(contest);
            }
            return contestDetailsLst;
        }
    }
}

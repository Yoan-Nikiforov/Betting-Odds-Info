using UltraPlayTask.Data.Models;

namespace UltraPlayTask.ViewModels
{
    public class MatchViewModel
    {
        public MatchViewModel(Match matchDto)
        {
            Id = matchDto.ID;
            FirstTeamName = matchDto.Name.Split('-')[0];
            SecondTeamName = matchDto.Name.Split('-')[1];
            MatchType = matchDto.MatchType.ToString();
            Bets = matchDto.Bets.Select(b => new BetViewModel(b)).OrderByDescending(x => x.SpecialOdds?.Count).ToList();
            StartDate = matchDto.StartDate.ToString("dd MMMM HH:mm");
        }
        public string Id { get; set; }
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public string StartDate { get; set; }
        public string MatchType { get; set; }
        public List<BetViewModel> Bets { get; set; }
        public int BetsCount => Bets.Count();
    }
}
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.ViewModels
{
    public class BetViewModel
    {
        public BetViewModel(Bet betDto)
        {
            Id = betDto.ID;
            Name = betDto.Name;
            Odds = betDto.Odds.Where(x => double.Parse(x.SpecialBetValue) <= 0).Select(o => new OddViewModel(o)).ToList();
            SpecialOdds = betDto.Odds.Where(x => double.Parse(x.SpecialBetValue) > 0)?.Select(x => new OddViewModel(x))?.GroupBy(x => x.SpecialBetValue)?.ToList();
            HasSpecialOdds = SpecialOdds?.Count > 0;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<OddViewModel> Odds { get; set; }
        public List<IGrouping<string, OddViewModel>> SpecialOdds { get; set; }

        public bool HasSpecialOdds { get; }
    }
}
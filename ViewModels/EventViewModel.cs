using UltraPlayTask.Data.Models;

namespace UltraPlayTask.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(Event eventDto)
        {
            Id = eventDto.ID;
            Name = eventDto.Name;
            Matches = eventDto.Matches.Where(x => x.StartDate.CompareTo(DateTime.Today.AddDays(1)) < 0).Select(m => new MatchViewModel(m));
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MatchViewModel> Matches { get; set; }
    }
}
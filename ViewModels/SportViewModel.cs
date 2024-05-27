using UltraPlayTask.Data.Models;

namespace UltraPlayTask.ViewModels
{
    public class SportViewModel
    {
        public SportViewModel(Sport sportDto)
        {
            Id = sportDto.ID;
            Name = sportDto.Name;
            Events = sportDto.Events?.Select(e => new EventViewModel(e))?.Where(x => x.Matches?.Count() > 0) ?? new List<EventViewModel>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}

using UltraPlayTask.Data.Models;

namespace UltraPlayTask.ViewModels
{
    public class OddViewModel
    {
        public OddViewModel(Odd oddDto)
        {
            Id = oddDto.ID;
            Value = double.Parse(oddDto.Value);
            SpecialBetValue = oddDto.SpecialBetValue;
            Name = oddDto.Name;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public double Value { get; set; }
        public string SpecialBetValue { get; set; }
    }
}
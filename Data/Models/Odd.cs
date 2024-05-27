using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{
    public class Odd : BaseDataModel
    {
        [XmlAttribute("Value")]
        public string Value { get; set; }

        [XmlAttribute("SpecialBetValue")]
        public string SpecialBetValue { get; set; } = "0";

        [ForeignKey("Bet")]
        public string BetID { get; set; }

        public Bet Bet { get; set; }
    }
}
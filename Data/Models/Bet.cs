using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{
    public class Bet : BaseDataModel
    {
        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Odd")]
        public List<Odd> Odds { get; set; }

        [Required]
        public string MatchID { get; set; }

        [Required]
        public Match Match { get; set; }
    }
}

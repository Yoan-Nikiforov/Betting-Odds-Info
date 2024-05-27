using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using UltraPlayTask.Business.Enums;

namespace UltraPlayTask.Data.Models
{
    public class Match : BaseDataModel
    {
        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public SportsMatchType MatchType { get; set; }

        [XmlElement("Bet")]
        public List<Bet> Bets { get; set; }

        [ForeignKey("Event")]
        public string EventID { get; set; }

        public Event Event { get; set; }
    }
}
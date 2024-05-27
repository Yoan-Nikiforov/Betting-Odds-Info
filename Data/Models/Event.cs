using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{
    public class Event : BaseDataModel
    {
        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryID")]
        public int CategoryID { get; set; }

        [XmlElement("Match")]
        public List<Match> Matches { get; set; }

        [Required]
        public string SportID { get; set; }

        [Required]
        public Sport Sport { get; set; }
    }
}
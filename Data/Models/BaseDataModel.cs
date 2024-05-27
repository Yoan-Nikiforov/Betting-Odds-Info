using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{
    public abstract class BaseDataModel
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [Key]
        [XmlAttribute("ID")]
        public string ID { get; set; }
    }
}

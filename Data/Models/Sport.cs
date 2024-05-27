using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{
    public class Sport : BaseDataModel
    {
        [XmlElement("Event")]
        public List<Event> Events { get; set; }
    }
}
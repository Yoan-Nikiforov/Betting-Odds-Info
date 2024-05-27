using System.Xml.Serialization;

namespace UltraPlayTask.Data.Models
{    
    //Added only because the xml has a non-standard root element. Not a dbset.
    public class XmlSports : BaseDataModel
    {
        [XmlElement("Sport")]
        public List<Sport> Sports { get; set; }
    }
}

using System.Xml.Serialization;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Business.Helpers
{
    public class XmlHelper
    {
        public static T ParseData<T>(string content) where T : BaseDataModel
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(content))
            {
                var deserializedData = (T)serializer.Deserialize(reader);
                return deserializedData;
            }
        }
    }
}

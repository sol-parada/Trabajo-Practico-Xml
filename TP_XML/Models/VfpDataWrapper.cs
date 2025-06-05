using System.Collections.Generic;
using System.Xml.Serialization;

namespace TP_XML.Models
{
    [XmlRoot("VFPData")]
    public class VfpDataWrapper<T>
    {
        [XmlElement("_expxml")]
        public List<T> Items { get; set; } = new List<T>();
    }
}
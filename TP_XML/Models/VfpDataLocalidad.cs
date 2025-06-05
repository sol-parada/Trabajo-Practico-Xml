using System.Xml.Serialization;

namespace TP_XML.Models
{
    [XmlRoot("VFPData")]
    public class VfpDataLocalidad
    {
        [XmlElement("_exportar")]
        public List<Localidad>? Items { get; set; }
    }
}
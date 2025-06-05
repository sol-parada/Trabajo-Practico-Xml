using System.Xml.Serialization;

namespace TP_XML.Models
{
    public class Especialidad
    {
        [XmlElement("especialidad")]
        public int Id { get; set; }

        [XmlElement("nombre")]
        public string? Nombre { get; set; }
    }
}
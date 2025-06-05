using System.Xml.Serialization;

namespace TP_XML.Models
{
    public class Grado
    {
        [XmlElement("grado")]
        public int Id { get; set; }

        [XmlElement("nombre")]
        public string? Nombre { get; set; }
    }
}
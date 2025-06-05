using System.Xml.Serialization;

namespace TP_XML.Models
{
    public class Plan
    {
        [XmlElement("especialidad")]
        public int Especialidad { get; set; }

        [XmlElement("plan")]
        public int Id { get; set; }

        [XmlElement("nombre")]
        public string? Nombre { get; set; }
    }
}
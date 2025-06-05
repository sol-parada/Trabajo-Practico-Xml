using System.Xml.Serialization;

namespace TP_XML.Models
{
    public class Materia
    {
        [XmlElement("especialidad")]
        public int Especialidad { get; set; }

        [XmlElement("plan")]
        public int Plan { get; set; }

        [XmlElement("materia")]
        public int Id { get; set; }

        [XmlElement("nombre")]
        public string? Nombre { get; set; }

        [XmlElement("ano")]
        public int? Ano { get; set; }
    }
}
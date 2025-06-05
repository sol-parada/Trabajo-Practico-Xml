using System.Xml.Serialization;

namespace TP_XML.Models
{
    public class Localidad
    {
        [XmlElement("codigo")]
        public int Id { get; set; }

        [XmlElement("ciudad")]
        public string? Ciudad { get; set; }

        [XmlElement("provincia")]
        public string? Provincia { get; set; }

        [XmlElement("pais_del_c")]
        public string? Pais { get; set; }

        public string? Nombre => Ciudad;
    }
}
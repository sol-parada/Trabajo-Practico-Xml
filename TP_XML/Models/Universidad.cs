using System.Xml.Serialization;
public class Universidad
{
    [XmlElement("universida")]
    public int Id { get; set; }

    [XmlElement("nombre")]
    public string? Nombre { get; set; }
}

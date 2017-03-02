using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("DialougeData")]
public class DialougeData
{
    [XmlArray("Dialouges")]
    [XmlArrayItem("Dialouge")]
    public List<Dialouge> Dialouges = new List<Dialouge>();
}
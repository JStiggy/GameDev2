using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class Dialouge {
    [XmlArrayItem("text")]
    public string[] text;
    public int autoScroll;
}

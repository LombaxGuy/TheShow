using System.Xml;
using System.Xml.Serialization;

public class Subtitle
{
    //This is where the attributes and elements of the xml file are defined
    [XmlAttribute("name")]
    public string name;

    [XmlElement("voiceLine")]
    public string voiceLine;
}

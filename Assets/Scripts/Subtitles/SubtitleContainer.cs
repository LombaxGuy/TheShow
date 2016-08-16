using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("SubtitleCollection")]
public class SubtitleContainer
{

    [XmlArray("Subtitles"), XmlArrayItem("Subtitle")]
    public List<Subtitle> subtitles = new List<Subtitle>();
    /// <summary>
    /// Loads the subtitle xml file and reads it's content 
    /// </summary>
    /// <returns></returns>
    public static SubtitleContainer LoadSubtitle()
    {
        TextAsset asset = Resources.Load<TextAsset>("UI/Subtitles/SubtitleCollection");

        XmlSerializer serializer = new XmlSerializer(typeof(SubtitleContainer));

        return serializer.Deserialize(new StringReader(asset.text)) as SubtitleContainer;
    }
}
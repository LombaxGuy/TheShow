﻿using UnityEngine;
using System.Collections.Generic;
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
    public static SubtitleContainer LoadSubtitle(string levelName)
    {

        TextAsset asset = Resources.Load<TextAsset>("UI/Subtitles/" + levelName + "Collection");

        XmlSerializer serializer = new XmlSerializer(typeof(SubtitleContainer));

        return serializer.Deserialize(new StringReader(asset.text)) as SubtitleContainer;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class TestSerialize
{

    [XmlAttribute("Id")]
    public int Id { get; set; }

    [XmlAttribute]
    public string Name { get; set; }

    [XmlElement]
    public List<int> list { get; set; }
}
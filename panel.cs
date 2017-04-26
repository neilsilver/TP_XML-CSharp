using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace ConvertXmlToCSharpClasses
{
    [XmlRoot(ElementName = "Platform")]
    public class Platform
    {
        [XmlElement(ElementName = "OS")]
        public string OS { get; set; }
        [XmlElement(ElementName = "DeviceName")]
        public string DeviceName { get; set; }
        [XmlElement(ElementName = "DisplayUnit")]
        public string DisplayUnit { get; set; }
        [XmlElement(ElementName = "DisplayWidth")]
        public string DisplayWidth { get; set; }
        [XmlElement(ElementName = "DisplayHeight")]
        public string DisplayHeight { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Information")]
    public class Information
    {
        [XmlElement(ElementName = "ToolName")]
        public string ToolName { get; set; }
        [XmlElement(ElementName = "ToolVersion")]
        public string ToolVersion { get; set; }
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
    }

    [XmlRoot(ElementName = "ZOrder")]
    public class ZOrder
    {
        [XmlAttribute(AttributeName = "Fixed")]
        public string Fixed { get; set; }
    }

    [XmlRoot(ElementName = "Application")]
    public class Application
    {
        [XmlElement(ElementName = "Platform")]
        public Platform Platform { get; set; }
        [XmlElement(ElementName = "Information")]
        public Information Information { get; set; }
        [XmlElement(ElementName = "Setting")]
        public string Setting { get; set; }
        [XmlElement(ElementName = "ZOrder")]
        public ZOrder ZOrder { get; set; }
        [XmlElement(ElementName = "RootPage")]
        public string RootPage { get; set; }
        [XmlAttribute(AttributeName = "Debug")]
        public string Debug { get; set; }
    }

    [XmlRoot(ElementName = "X")]
    public class X
    {
        [XmlAttribute(AttributeName = "Pixels")]
        public string Pixels { get; set; }
    }

    [XmlRoot(ElementName = "Y")]
    public class Y
    {
        [XmlAttribute(AttributeName = "Pixels")]
        public string Pixels { get; set; }
    }

    [XmlRoot(ElementName = "Position")]
    public class Position
    {
        [XmlElement(ElementName = "X")]
        public X X { get; set; }
        [XmlElement(ElementName = "Y")]
        public Y Y { get; set; }
    }

    [XmlRoot(ElementName = "Width")]
    public class Width
    {
        [XmlAttribute(AttributeName = "Pixels")]
        public string Pixels { get; set; }
    }

    [XmlRoot(ElementName = "Height")]
    public class Height
    {
        [XmlAttribute(AttributeName = "Pixels")]
        public string Pixels { get; set; }
    }

    [XmlRoot(ElementName = "Size")]
    public class Size
    {
        [XmlElement(ElementName = "Width")]
        public Width Width { get; set; }
        [XmlElement(ElementName = "Height")]
        public Height Height { get; set; }
    }

    [XmlRoot(ElementName = "Color")]
    public class Color
    {
        [XmlAttribute(AttributeName = "Background")]
        public string Background { get; set; }
    }

    [XmlRoot(ElementName = "VisualStyle")]
    public class VisualStyle
    {
        [XmlElement(ElementName = "Color")]
        public Color Color { get; set; }
    }

    [XmlRoot(ElementName = "DigitalJoin")]
    public class DigitalJoin
    {
        [XmlElement(ElementName = "Visibility_Digital_Join")]
        public string Visibility_Digital_Join { get; set; }
        [XmlElement(ElementName = "Press_Digital_Join")]
        public string Press_Digital_Join { get; set; }
        [XmlElement(ElementName = "Enable_Digital_Join")]
        public string Enable_Digital_Join { get; set; }
    }

    [XmlRoot(ElementName = "Control")]
    public class Control
    {
        [XmlElement(ElementName = "Position")]
        public Position Position { get; set; }
        [XmlElement(ElementName = "Size")]
        public Size Size { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Page_Flip")]
        public string Page_Flip { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Index")]
        public string Index { get; set; }
        [XmlElement(ElementName = "DigitalJoin")]
        public DigitalJoin DigitalJoin { get; set; }
        [XmlElement(ElementName = "SerialJoin")]
        public SerialJoin SerialJoin { get; set; }
        [XmlElement(ElementName = "AnalogJoin")]
        public AnalogJoin AnalogJoin { get; set; }
        [XmlElement(ElementName = "SerialJoin_cips")]
        public SerialJoin_cips SerialJoin_cips { get; set; }
        [XmlElement(ElementName = "AnalogJoin_cipa")]
        public AnalogJoin_cipa AnalogJoin_cipa { get; set; }
    }

    [XmlRoot(ElementName = "Page")]
    public class Page
    {
        [XmlElement(ElementName = "Position")]
        public Position Position { get; set; }
        [XmlElement(ElementName = "Size")]
        public Size Size { get; set; }
        [XmlElement(ElementName = "VisualStyle")]
        public VisualStyle VisualStyle { get; set; }
        [XmlElement(ElementName = "Theme")]
        public string Theme { get; set; }
        [XmlElement(ElementName = "DigitalJoin")]
        public DigitalJoin DigitalJoin { get; set; }
        [XmlElement(ElementName = "Control")]
        public List<Control> Control { get; set; }
        [XmlElement(ElementName = "Smart_Object_ID")]
        public List<string> Smart_Object_ID { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "SerialJoin")]
    public class SerialJoin
    {
        [XmlElement(ElementName = "Indirect_Text_Serial_Join")]
        public string Indirect_Text_Serial_Join { get; set; }
        [XmlElement(ElementName = "Output_Text_Serial_Join")]
        public string Output_Text_Serial_Join { get; set; }
    }

    [XmlRoot(ElementName = "AnalogJoin")]
    public class AnalogJoin
    {
        [XmlElement(ElementName = "Touch_Feedback_Analog_Join")]
        public string Touch_Feedback_Analog_Join { get; set; }
    }

    [XmlRoot(ElementName = "SerialJoin_cips")]
    public class SerialJoin_cips
    {
        [XmlElement(ElementName = "CIPS")]
        public string CIPS { get; set; }
    }

    [XmlRoot(ElementName = "AnalogJoin_cipa")]
    public class AnalogJoin_cipa
    {
        [XmlElement(ElementName = "CIPA")]
        public string CIPA { get; set; }
    }

    [XmlRoot(ElementName = "File")]
    public class File
    {
        [XmlElement(ElementName = "Application")]
        public Application Application { get; set; }
        [XmlElement(ElementName = "Page")]
        public List<Page> Page { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
    }


}




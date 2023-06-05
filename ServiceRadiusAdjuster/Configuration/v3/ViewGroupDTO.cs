using System.Collections.Generic;
using System.Xml.Serialization;

namespace ServiceRadiusAdjuster.Configuration.v3
{
    [XmlRoot("ViewGroup")]
    public class ViewGroupDto
    {
        public string? Name { get; set; }
        public int Order { get; set; }
        [XmlElement("OptionItems")]
        public List<OptionItemDto>? OptionItemDtos { get; set; }
    }
}

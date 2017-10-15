using System.Collections.Generic;
using System.Xml.Serialization;

namespace ServiceRadiusAdjuster.Configuration.v3
{
    [XmlRoot("Profile")]
    public class ProfileDto
    {
        public string Version { get; set; }
        [XmlElement("ViewGroups")]
        public List<ViewGroupDto> ViewGroupDtos { get; set; }
    }
}

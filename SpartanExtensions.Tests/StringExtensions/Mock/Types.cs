using System.Xml.Serialization;

namespace SpartanExtensions.Tests.StringExtensions.Mock
{
    public enum Types
    {
        [XmlEnum("type-one")]
        TypeOne,

        [XmlEnum("type-two")]
        TypeTwo
    }
}

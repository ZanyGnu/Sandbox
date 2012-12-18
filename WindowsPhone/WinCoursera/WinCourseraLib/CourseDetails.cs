
namespace CourseraLib
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CourseDetails
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string large_icon { get; set; }

        [DataMember]
        public string short_description { get; set; }

        [DataMember]
        public string instructor { get; set; }
    }
}

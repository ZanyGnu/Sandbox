
namespace CourseraLib
{
    using System.Data.Linq.Mapping;
    using System.Runtime.Serialization;

    [DataContract]
    [Table]
    public class CourseDetails
    {
        [DataMember]
        [Column]
        public string id { get; set; }

        [DataMember]
        [Column]
        public string short_name { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string large_icon { get; set; }

        [DataMember]
        public string short_description { get; set; }

        [DataMember]
        public string instructor { get; set; }

        [DataMember]
        public string about_the_course { get; set; }

        [DataMember]
        public string faq { get; set; }

        [DataMember]
        public string recommended_background { get; set; }        
    }
}

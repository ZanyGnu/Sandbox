
namespace CourseraLib
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UniversityDetails
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string logo { get; set; }

        [DataMember]
        public string home_link { get; set; }

        [DataMember]
        public string banner { get; set; }

        [DataMember]
        public string short_name { get; set; }
    }
}

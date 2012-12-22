
namespace CourseraLib
{
    using System.Data.Linq.Mapping;
    using System.Runtime.Serialization;

    [DataContract]
    [Table]
    public class UniversityDetails
    {
        [DataMember]
        [Column]
        public string id { get; set; }

        [DataMember]
        [Column]
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


namespace CourseraLib
{
    using System.Data.Linq;

    class CourseraDataContext: DataContext
    {
        public CourseraDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<CourseDetails> Courses;

        public Table<UniversityDetails> Universities;
    }
}

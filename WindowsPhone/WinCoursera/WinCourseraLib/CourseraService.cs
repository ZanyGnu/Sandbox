
namespace CourseraLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Threading.Tasks;

    public delegate void HandleResults<T>(T results);
    public delegate void HandleError(Exception exception);

    public class CourseraWebService
    {
        private static JsonRequestHandler JsonRequestHandler = new JsonRequestHandler();
        private static FeedRequestHandler FeedRequestHandler = new FeedRequestHandler();

        public static void GetAllCoursesAsync(HandleResults<List<CourseDetails>> handleResults)
        {
            JsonRequestHandler.InvokeApiAsync<List<CourseDetails>>(handleResults, ApiEndpoints.CourseList);
        }

        public static void GetCourseInfoAsync(HandleResults<CourseDetails> handleResults, string courseId)
        {
            JsonRequestHandler.InvokeApiAsync<CourseDetails>(handleResults, ApiEndpoints.CourseInfo + courseId);
        }

        public static void GetAllUniversitiesAsync(HandleResults<List<UniversityDetails>> handleResults)
        {
            JsonRequestHandler.InvokeApiAsync<List<UniversityDetails>>(handleResults, ApiEndpoints.UniversityList);
        }

        public static void GetCourseraBlogSyndication(HandleResults<SyndicationFeed> handleResults)
        {
            FeedRequestHandler.InvokeApiAsync<SyndicationFeed>(handleResults, ApiEndpoints.CourseraBlogFeed);
        }
    }
}

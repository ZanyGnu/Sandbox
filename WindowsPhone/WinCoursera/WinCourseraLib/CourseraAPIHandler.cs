
namespace CourseraLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;

    public class CourseraAPIHandler
    {
        public delegate void HandleResults<T>(T courses);
        
        public static void GetAllCoursesAsync<T>(HandleResults<T> handleResults) where T : class, new()
        {
            InvokeApiAsync<T>(handleResults, ApiEndpoints.CourseList);
        }

        public static void GetCourseInfoAsync<T>(HandleResults<T> handleResults, string courseId) where T:class, new ()
        {
            InvokeApiAsync<T>(handleResults, ApiEndpoints.CourseInfo + courseId);
        }

        private static void InvokeApiAsync<T>(HandleResults<T> handleResults, string endpointUri) where T:class, new ()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpointUri);
            request.Method = "GET";
            RequestHandler<T> requestResults = new RequestHandler<T>() { Request = request, HandleResults = handleResults };
            request.BeginGetResponse(new AsyncCallback(OnGetResponse<T>), requestResults);            
        }

        private static void OnGetResponse<T>(IAsyncResult asyncResult) where T : class, new()
        {
            RequestHandler<T> requestResults = (RequestHandler<T>)asyncResult.AsyncState;
            HttpWebRequest myRequest = requestResults.Request;
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(asyncResult);

            using (StreamReader httpwebStreamReader = new StreamReader(myResponse.GetResponseStream()))
            {
                string results = httpwebStreamReader.ReadToEnd();
                T serializedResults = SerializeJsonResults<T>(results);
                requestResults.HandleResults(serializedResults);
            }

            myResponse.Close();
        }
        
        private static T SerializeJsonResults<T>(string resultJson) where T: class, new()
        {
            T obj = new T();
            MemoryStream readStream = new MemoryStream();
            DataContractJsonSerializer readSer = new DataContractJsonSerializer(obj.GetType());
            byte[] byteRead;

            // Convert string read into byte array.
            byteRead = System.Text.Encoding.UTF8.GetBytes(resultJson);

            // Write the byte array to the stream.
            readStream.Position = 0;

            readStream.Write(byteRead, 0, byteRead.Length);

            obj = (T)readSer.ReadObject(readStream);

            if (obj != null)
            {
                return obj;
            }

            return default(T);
        }
        
        private class RequestHandler<T>
        {
            public HttpWebRequest Request;
            public HandleResults<T> HandleResults;
        }
    }
}


namespace CourseraLib
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    internal class JsonRequestHandler : WebRequestHandler
    {
        protected override void HandleResponse<T>(RequestHandler<T> requestResults, StreamReader httpwebStreamReader)
        {
            string results = httpwebStreamReader.ReadToEnd();
            T serializedResults = SerializeJsonResults<T>(results);
            requestResults.ResultsHandler(serializedResults);
        }

        internal static T SerializeJsonResults<T>(string resultJson) where T : class, new()
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
    }
}

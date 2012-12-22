
namespace CourseraLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.ServiceModel.Syndication;

    internal abstract class WebRequestHandler
    {        
        internal void InvokeApiAsync<T>(HandleResults<T> handleResults, string endpointUri) where T:class, new ()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpointUri);
            request.Method = "GET";
            RequestHandler<T> requestResults = new RequestHandler<T>() { Request = request, ResultsHandler = handleResults };
            request.BeginGetResponse(new AsyncCallback(OnGetResponse<T>), requestResults);            
        }

        internal void OnGetResponse<T>(IAsyncResult asyncResult) where T : class, new()
        {
            RequestHandler<T> requestResults = (RequestHandler<T>)asyncResult.AsyncState;
            HttpWebRequest myRequest = requestResults.Request;
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(asyncResult);

            using (StreamReader httpwebStreamReader = new StreamReader(myResponse.GetResponseStream()))
            {
                HandleResponse<T>(requestResults, httpwebStreamReader);
            }

            myResponse.Close();
        }

        protected abstract void HandleResponse<T>(RequestHandler<T> requestResults, StreamReader httpwebStreamReader) where T : class, new();

        protected class RequestHandler<T>
        {
            public HttpWebRequest Request;
            public HandleResults<T> ResultsHandler;
            public HandleError ErrorHandler;
        }
    }
}

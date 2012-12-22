
namespace CourseraLib
{
    using System.IO;
    using System.ServiceModel.Syndication;
    using System.Xml;

    internal class FeedRequestHandler : WebRequestHandler
    {
        protected override void HandleResponse<T>(WebRequestHandler.RequestHandler<T> requestResults, StreamReader httpwebStreamReader)
        {
            XmlReader xmlReader = XmlReader.Create(httpwebStreamReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            requestResults.ResultsHandler(feed as T);
        }
    }
}

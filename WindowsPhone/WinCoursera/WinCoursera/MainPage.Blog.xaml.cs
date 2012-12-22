
namespace WinCoursera
{
    using CourseraLib;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Tasks;
    using System;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainPage : PhoneApplicationPage
    {
        private void BlogPanoromaItem_Loaded(object sender, RoutedEventArgs e)
        {
            CourseraWebService.GetCourseraBlogSyndication(
                    delegate(SyndicationFeed feed)
                    {
                        this.Dispatcher.BeginInvoke(() =>
                        {
                            feedListBox.ItemsSource = feed.Items;
                        });
                    });

        }

        // The SelectionChanged handler for the feed items  
        private void feedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                // Get the SyndicationItem that was tapped. 
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                // Set up the page navigation only if a link actually exists in the feed item. 
                if (sItem.Links.Count > 0)
                {
                    // Get the associated URI of the feed item. 
                    Uri uri = sItem.Links.FirstOrDefault().Uri;

                    // Create a new WebBrowserTask Launcher to navigate to the feed item.  
                    // An alternative solution would be to use a WebBrowser control, but WebBrowserTask is simpler to use.  
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = uri;
                    webBrowserTask.Show();
                }
            }
        }
    }
}

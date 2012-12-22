
namespace WinCoursera
{
    using CourseraLib;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainPage : PhoneApplicationPage
    {

        private void CoursesPanoromaItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (CourseResultsData.DataContext != null)
            {
                // if we've already loaded the data, then lets not refresh it unnecessarily
                return;
            }

            RefreshCourseResultsAsync();
        }

        private void RefreshCourseResultsAsync()
        {
            // If there is an older DataContext set, let it remain,
            // this will not blank out the UI on the user.

            // Tell the user we are trying to download
            // (The download itself takes time given the large amount of data returned back)
            // TODO: Find a way to page the data back and render appropriately in chunks
            SystemTray.SetProgressIndicator(this, DownloadInProgress);

            // Invoke the json api asynchronously.
            CourseraWebService.GetAllCoursesAsync(
                delegate(List<CourseDetails> results)
                {
                    // We now have the results of the json API call.
                    // Dispatch work on the UI thread to have it render it.
                    Dispatcher.BeginInvoke(delegate
                    {
                        CourseResultsData.DataContext = results;

                        // Let the user know that we are trying to render the data.
                        // (The render also takes time given the large number of results.)
                        // Once the rendering is complete, the CourseResultsData_LayoutUpdated 
                        // event handler is invoked, where we will reset the progress bar.

                        SystemTray.SetProgressIndicator(this, RenderInProgress);
                    });
                });
        }

        private void CourseResultsData_Tap(object sender, GestureEventArgs e)
        {
            CourseDetails course = ((sender as ListBox).SelectedValue as CourseDetails);

            NavigationService.Navigate(
                new Uri(String.Format(
                    "/CourseDetailsPage.xaml?topic-id={0}",
                    course.short_name),
                UriKind.Relative));
        }

        private void CourseResultsData_LayoutUpdated(object sender, EventArgs e)
        {
            SystemTray.SetProgressIndicator(this, ProgressComplete);
        }

        private void CourseDescription_Tap(object sender, GestureEventArgs e)
        {
            CourseDetails course = ((sender as ListBox).SelectedValue as CourseDetails);

            NavigationService.Navigate(
                new Uri(String.Format(
                    "/CourseDetailsPage.xaml?topic-id={0}",
                    course.short_name),
                UriKind.Relative));
        }


    }
}

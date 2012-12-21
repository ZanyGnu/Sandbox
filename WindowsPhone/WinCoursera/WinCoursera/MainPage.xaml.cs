
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
        private static ProgressIndicator DownloadInProgress = new ProgressIndicator
        {
            IsVisible = true,
            IsIndeterminate = true,
            Text = "Downloading ..."
        };

        private static ProgressIndicator RenderInProgress = new ProgressIndicator
        {
            IsVisible = true,
            IsIndeterminate = true,
            Text = "Rendering ..."
        };

        private static ProgressIndicator ProgressComplete = new ProgressIndicator
        {
            IsVisible = false,
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void CoursesPivotItem_Loaded(object sender, RoutedEventArgs e)
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
            CourseraAPIHandler.GetAllCoursesAsync(
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

        private void UniversityResultsData_Tap(object sender, GestureEventArgs e)
        {
            UniversityDetails univ = ((sender as ListBox).SelectedValue as UniversityDetails);

            //NavigationService.Navigate(
            //    new Uri(String.Format(
            //        "/UniversityDetailsPage.xaml?university-id={0}", 
            //        univ.short_name), 
            //    UriKind.Relative));
        }

        private void UniversitiesPivotItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (UniversityResultsData.DataContext != null)
            {
                // if we've already loaded the data, then lets not refresh it unnecessarily
                return;
            }

            RefreshUniversitiesResultsAsync();
        }

        private void RefreshUniversitiesResultsAsync()
        {
            // Invoke the json api asynchronously.            
            CourseraAPIHandler.GetAllUniversitiesAsync(
                delegate(List<UniversityDetails> results)
                {
                    // We now have the results of the json API call.
                    // Dispatch work on the UI thread to have it render it.
                    Dispatcher.BeginInvoke(delegate
                    {
                        UniversityResultsData.DataContext = results;
                    });
                });
        }

        private void CourseResultsData_LayoutUpdated(object sender, EventArgs e)
        {
            SystemTray.SetProgressIndicator(this, ProgressComplete);
        }

        private void RefreshAppBarButton_Click(object sender, EventArgs e)
        {
            RefreshCourseResultsAsync();
            RefreshUniversitiesResultsAsync();
        }

        private void SearchAppBarButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}

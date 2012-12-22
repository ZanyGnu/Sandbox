
namespace WinCoursera
{
    using CourseraLib;
    using Microsoft.Phone.Controls;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainPage : PhoneApplicationPage
    {
        private void UniversityResultsData_Tap(object sender, GestureEventArgs e)
        {
            UniversityDetails univ = ((sender as ListBox).SelectedValue as UniversityDetails);

            //NavigationService.Navigate(
            //    new Uri(String.Format(
            //        "/UniversityDetailsPage.xaml?university-id={0}", 
            //        univ.short_name), 
            //    UriKind.Relative));
        }

        private void UniversitiesPanormaItem_Loaded(object sender, RoutedEventArgs e)
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
            CourseraWebService.GetAllUniversitiesAsync(
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
    }
}

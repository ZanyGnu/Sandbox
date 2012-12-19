
namespace WinCoursera
{
    using CourseraLib;
    using Microsoft.Phone.Controls;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchCourses_Click(object sender, RoutedEventArgs e)
        {
            CourseResultsLabel.Text = " loading results ...";
            CourseResultsData.DataContext = null;

            CourseraAPIHandler.GetAllCoursesAsync(
                delegate(List<CourseDetails> results)
                {
                    Dispatcher.BeginInvoke(delegate
                    {
                        CourseResultsData.DataContext = results;
                        CourseResultsLabel.Text = String.Empty;
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
    }
}

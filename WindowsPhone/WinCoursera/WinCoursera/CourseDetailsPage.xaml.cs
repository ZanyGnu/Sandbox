
namespace WinCoursera
{
    using System;
    using Microsoft.Phone.Controls;
    using System.Windows.Media.Imaging;
    using CourseraLib;
    using System.Windows.Media;
    using System.Windows;

    public partial class CourseDetailsPage : PhoneApplicationPage
    {
        public CourseDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string courseId;

            if (this.NavigationContext.QueryString.TryGetValue("topic-id", out courseId))
            {
                CourseraAPIHandler.GetCourseInfoAsync(
                    delegate(CourseDetails details)
                    {
                        this.Dispatcher.BeginInvoke(() =>  
                            {
                                this.DataContext = details;
                            });
                    },
                    courseId);
            }

            base.OnNavigatedTo(e);            
        }
    }
}


namespace WinCoursera
{
    using System;
    using Microsoft.Phone.Controls;
    using System.Windows.Media.Imaging;
    using CourseraLib;

    public partial class CourseDetailsPage : PhoneApplicationPage
    {
        public CourseDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string course_id;

            if (this.NavigationContext.QueryString.TryGetValue("topic-id", out course_id))
            {

            }

            this.DataContext = App.Course;
            base.OnNavigatedTo(e);            
        }
    }
}

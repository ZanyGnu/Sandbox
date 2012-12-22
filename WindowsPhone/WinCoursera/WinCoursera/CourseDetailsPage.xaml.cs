
namespace WinCoursera
{
    using CourseraLib;
    using Microsoft.Phone.Controls;

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
                CourseraWebService.GetCourseInfoAsync(
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

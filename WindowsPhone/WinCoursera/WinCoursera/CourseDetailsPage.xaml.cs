
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
                                // LoadDetailsPanaromaItem();
                            });
                    },
                    courseId);
            }

            base.OnNavigatedTo(e);            
        }

        //private void detailsBrowser_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    LoadDetailsPanaromaItem();
        //}

        //private void LoadDetailsPanaromaItem()
        //{
        //    CourseDetails cd = (CourseDetails)this.DataContext;
        //    string textDetails = (cd == null) ? string.Empty : cd.about_the_course;

        //    var htmlConcat = string.Format("<html>" +
        //          "<body style=\"margin:0px;padding:0px;background-color:{0};\">" +
        //          "<meta name=\"viewport\" content=\"width=320,user-scalable=no\" />" + 
        //          "<div id=\"pageWrapper\" style=\"width:100%; color:{1}; " +
        //          "background-color:{0}\">{2}</div></body></html>",
        //          FetchBackGroundColor(),
        //          FetchFontColor(),
        //          textDetails);

        //    detailsBrowser.NavigateToString(htmlConcat);
        //}

        //private string FetchFontColor()
        //{
        //    return IsBackgroundBlack() ? "#fff;" : "#000";
        //}

        //private static bool IsBackgroundBlack()
        //{
        //    return FetchBackGroundColor() == "#FF000000";
        //}

        //private static string FetchBackGroundColor()
        //{
        //    string color;
        //    Color mc = (Color)Application.Current.Resources["PhoneBackgroundColor"];
        //    color = mc.ToString();
        //    return color;
        //}


    }
}


namespace WinCoursera
{
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using System;

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

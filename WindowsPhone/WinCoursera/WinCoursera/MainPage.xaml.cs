/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.UserData;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using CourseraLib;

namespace WinCoursera
{
    public partial class MainPage : PhoneApplicationPage
    {
        FilterKind CourseFilterKind = FilterKind.None;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // name.IsChecked = true;

            //CourseAccounts.DataContext = (new Courses()).Accounts;
            //CalendarAccounts.DataContext = (new Appointments()).Accounts;
        }


        private void SearchCourses_Click(object sender, RoutedEventArgs e)
        {
            //Add code to validate the CourseFilterString.Text input.
            
            CourseResultsLabel.Text = "results are loading...";
            CourseResultsData.DataContext = null;

            //Contacts cons = new Contacts();

            //cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(Contacts_SearchCompleted);

            //cons.SearchAsync(contactFilterString.Text, contactFilterKind, "Contacts Test #1");

            DoHttpWebRequest("https://www.coursera.org/maestro/api/topic/list?full=1");

        }

        public void DoHttpWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.BeginGetResponse(new AsyncCallback(onGetResponse), request);
        }

        public void onGetResponse(IAsyncResult asyncResult)
        {
            HttpWebRequest myRequest = (HttpWebRequest)asyncResult.AsyncState;
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(asyncResult);

            using (StreamReader httpwebStreamReader = new StreamReader(myResponse.GetResponseStream()))
            {
                string results = httpwebStreamReader.ReadToEnd();
                Dispatcher.BeginInvoke(() => CourseResultsData.DataContext = RenderJsonResults(results));
            }
            myResponse.Close();
        }

        List<CourseDetails> RenderJsonResults(string resultJson)
        {
            List<CourseDetails> obj = new List<CourseDetails>();
            MemoryStream readStream = new MemoryStream();                                           //Obtain stream for reading the object
            DataContractJsonSerializer readSer = new DataContractJsonSerializer(obj.GetType());     //Declare DataContractSerializer object to serialise JSON data
            byte[] byteRead;

            //Convert string read into byte array 
            byteRead = System.Text.Encoding.UTF8.GetBytes(resultJson);

            //Write the byte array to the stream
            readStream.Position = 0;

            readStream.Write(byteRead, 0, byteRead.Length);

            obj = (List<CourseDetails>)readSer.ReadObject(readStream);

            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        //void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        //{
        //    //MessageBox.Show(e.State.ToString());

        //    try
        //    {
        //        //Bind the results to the list box that displays them in the UI
        //        ContactResultsData.DataContext = e.Results;
        //    }
        //    catch (System.Exception)
        //    {
        //        //That's okay, no results
        //    }

        //    if (ContactResultsData.Items.Count > 0)
        //    {
        //        ContactResultsLabel.Text = "results (tap name for details...)";
        //    }
        //    else
        //    {
        //        ContactResultsLabel.Text = "no results";
        //    }
        //}


        private void CourseResultsData_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CourseDetailsPage.xaml", UriKind.Relative));
        }


        //private void FilterChange(object sender, RoutedEventArgs e)
        //{
        //    String option = ((RadioButton)sender).Name;

        //    InputScope scope = new InputScope();
        //    InputScopeName scopeName = new InputScopeName();

        //    switch (option)
        //    {
        //        case "name":
        //            CourseFilterKind = FilterKind.DisplayName;
        //            scopeName.NameValue = InputScopeNameValue.Text;
        //            break;

        //        case "phone":
        //            CourseFilterKind = FilterKind.PhoneNumber;
        //            scopeName.NameValue = InputScopeNameValue.TelephoneNumber;
        //            break;

        //        case "email":
        //            CourseFilterKind = FilterKind.EmailAddress;
        //            scopeName.NameValue = InputScopeNameValue.EmailSmtpAddress;
        //            break;

        //        default:
        //            CourseFilterKind = FilterKind.None;
        //            break;
        //    }

        //    scope.Names.Add(scopeName);
        //    CourseFilterString.InputScope = scope;
        //    CourseFilterString.Focus();
        //}


        //private void SearchAppointments_Click(object sender, RoutedEventArgs e)
        //{
        //    AppointmentResultsLabel.Text = "results are loading...";
        //    AppointmentResultsData.DataContext = null;
        //    Appointments appts = new Appointments();

        //    appts.SearchCompleted += new EventHandler<AppointmentsSearchEventArgs>(Appointments_SearchCompleted);

        //    DateTime start = DateTime.Now;
        //    DateTime end = start.AddDays(7);

        //    appts.SearchAsync(start, end, 20, "Appointments Test #1");
        //}


        //void Appointments_SearchCompleted(object sender, AppointmentsSearchEventArgs e)
        //{
        //    StartDate.Text = e.StartTimeInclusive.ToShortDateString();
        //    EndDate.Text = e.EndTimeInclusive.ToShortDateString();

        //    try
        //    {
        //        //Bind the results to the list box that displays them in the UI
        //        AppointmentResultsData.DataContext = e.Results;
        //    }
        //    catch (System.Exception)
        //    {
        //        //That's okay, no results
        //    }

        //    if (AppointmentResultsData.Items.Count > 0)
        //    {
        //        AppointmentResultsLabel.Text = "results (tap for details...)";
        //    }
        //    else
        //    {
        //        AppointmentResultsLabel.Text = "no results";
        //    }
        //}


        //private void AppointmentResultsData_Tap(object sender, GestureEventArgs e)
        //{
        //    App.appt = ((sender as ListBox).SelectedValue as Appointment);

        //    NavigationService.Navigate(new Uri("/AppointmentDetails.xaml", UriKind.Relative));
        //}

    }//End page class


    //public class CoursePictureConverter : System.Windows.Data.IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        Course c = value as Course;
    //        if (c == null) return null;

    //        System.IO.Stream imageStream = c.GetPicture();
    //        if (null != imageStream)
    //        {
    //            return Microsoft.Phone.PictureDecoder.DecodeJpeg(imageStream);
    //        }
    //        return null;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}//End converter class


}//End namespace

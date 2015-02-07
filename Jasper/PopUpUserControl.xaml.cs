using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace Jasper
{
    public partial class PopUpUserControl : PhoneApplicationPage
    {
        public PopUpUserControl()
        {
            InitializeComponent();
        }

        public class onCreateNote
        {
            public int status { get; set; }
            public int Id { get; set; }
        }

        public class RootObjectonNoteCreate
        {
            public int NoteId { get; set; }
            public string content { get; set; }
            public List<string> shared { get; set; }
        }

        public async void CreateANewFile(object sender, EventArgs e)
        {
            if (fileNameL.Text == ""){
                return;
            }
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (store.GetDirectoryNames().Contains("./"))
            {
                string directory = "./";
                var isoFileStream = new IsolatedStorageFileStream(directory + "\\" + fileNameL.Text, FileMode.OpenOrCreate, store);
            }

            dbServices d = new dbServices();

            var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email", d.returnEmailofUser()),
                        new KeyValuePair<string, string>("name", fileNameL.Text)
                    };

            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response = await httpClient.PostAsync(urlConfig.createNoteUrl(), new FormUrlEncodedContent(values));

            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseString);
                System.Diagnostics.Debug.WriteLine("response: " + responseString);
                var responseStrings = JsonConvert.DeserializeObject<onCreateNote>(responseString);
                System.Diagnostics.Debug.WriteLine(responseStrings);
                if (responseStrings.status == 0)
                {
                    d.addnoteInDb((onCreateNote)responseStrings, fileNameL.Text);
                    NavigationService.Navigate(new Uri("/DocListing.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Possibly wrong password !!"); //Add Message box here.
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.ToString());
            }

            NavigationService.Navigate(new Uri("/Note.xaml?file=" + fileNameL.Text, UriKind.RelativeOrAbsolute));
        }

        public void DoNotCreateANewFile(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DocListing.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }
    }
}
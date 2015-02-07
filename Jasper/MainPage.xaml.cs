using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using Jasper.Resources;

namespace Jasper
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        public async void Signup(object sender, RoutedEventArgs e)
        {
            
            var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("firstname", firstname.Text),
                        new KeyValuePair<string, string>("lastname", lastname.Text),
                        new KeyValuePair<string, string>("email", email.Text),
                        new KeyValuePair<string, string>("username", username.Text),
                        new KeyValuePair<string, string>("password", password.Password)
                    };
            System.Diagnostics.Debug.WriteLine("Button pressed !!");
            System.Diagnostics.Debug.WriteLine(values);

            var httpClient = new HttpClient(new HttpClientHandler());
            urlConfig urlconfig = new urlConfig();
            HttpResponseMessage response = await httpClient.PostAsync(urlconfig.homeUrl(), new FormUrlEncodedContent(values));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
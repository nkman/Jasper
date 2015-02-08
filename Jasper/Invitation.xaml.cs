using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;

namespace Jasper
{
    public partial class Invitation : PhoneApplicationPage
    {
        public string filename { get; set; }
        public Invitation()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            filename = NavigationContext.QueryString["file"];
            System.Diagnostics.Debug.WriteLine(filename);
        }

        public async void sendInvite(object sender, EventArgs e)
        {
            string email = inviteEmail.Text;
            dbServices d = new dbServices();
            var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("id", d.getNoteId(filename).ToString())
                    };

            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response = await httpClient.PostAsync(urlConfig.inviteUrl(), new FormUrlEncodedContent(values));
            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseString);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.ToString());
            }
        }
    }
}
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
using Newtonsoft.Json;
using Jasper.Resources;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Jasper
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private DbDataContext userDB;
        private ObservableCollection<userData> _userDetail;

        public ObservableCollection<userData> userdata
        {
            get
            {
                return _userDetail;
            }
            set
            {
                if (_userDetail != value)
                {
                    NotifyPropertyChanging("users");
                    _userDetail = value;
                    NotifyPropertyChanged("users");
                }
            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            userDB = new DbDataContext(DbDataContext.DBConnectionString);
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

        public async void Login(object sender, RoutedEventArgs e)
        {
            var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email", EmailAddress.Text),
                        new KeyValuePair<string, string>("password", Password.Password)
                    };

            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response = await httpClient.PostAsync(urlConfig.loginUrl(), new FormUrlEncodedContent(values));
            System.Diagnostics.Debug.WriteLine("1");
            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseString);
                System.Diagnostics.Debug.WriteLine("response: " + responseString);
                var responseStrings = JsonConvert.DeserializeObject <SignupVariable>(responseString);
                System.Diagnostics.Debug.WriteLine(responseStrings);
                if (responseStrings.status == 0)
                {
                    adduserInDb((SignupVariable)responseStrings);
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

        }

        public class SignupVariable
        {
            public int status { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
        }

        public void Cha00nge(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("lll");

            this.NavigationService.Navigate(new Uri("/Note.xaml", UriKind.Relative));
        }

        public void MainPageBakchodi(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Note.xaml", UriKind.RelativeOrAbsolute));
        }

        public async void Signup(object sender, RoutedEventArgs e)
        {
            var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("firstname", firstname.Text),
                        new KeyValuePair<string, string>("lastname", lastname.Text),
                        new KeyValuePair<string, string>("email", email.Text),
                        new KeyValuePair<string, string>("password", password.Password)
                    };

            System.Diagnostics.Debug.WriteLine("Button pressed !!");
            System.Diagnostics.Debug.WriteLine(values);

            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response = await httpClient.PostAsync(urlConfig.homeUrl(), new FormUrlEncodedContent(values));
            System.Diagnostics.Debug.WriteLine("1");
            try
            {
                //response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseString);
                System.Diagnostics.Debug.WriteLine("response: " + responseString);
                var responseStrings = JsonConvert.SerializeObject(responseString);

                bool signupSuccess = false;
                foreach (string resp in responseStrings.Split(','))
                {
                    foreach (string entry in resp.Split(':'))
                    {
                        if (resp.Equals("Success"))
                        {
                            signupSuccess = true;
                            System.Diagnostics.Debug.WriteLine("Signup succesful");
                            break;
                        }
                    }
                }

                if (signupSuccess == false)
                {
                    System.Diagnostics.Debug.WriteLine("Signup Not - succesful");
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.ToString());
            }

        }

        public void adduserInDb(SignupVariable result)
        {
            var userInDB = from userData _user in userDB.userdata select _user;
            userdata = new ObservableCollection<userData>(userInDB);

            var dbData = userdata.ToList();

            string _t = JsonConvert.SerializeObject(dbData);
            System.Diagnostics.Debug.WriteLine(_t);

            userData newUser = new userData
            {
                email = result.email,
                name = result.firstname +" "+ result.lastname
            };

            try
            {
                NotifyPropertyChanging("userdata");
                userdata.Add(newUser);
                userDB.userdata.InsertOnSubmit(newUser);
                userDB.SubmitChanges();
                NotifyPropertyChanged("userdata");
                System.Diagnostics.Debug.WriteLine("Added user in database");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
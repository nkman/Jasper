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

namespace Jasper
{
    public partial class PopUpUserControl : PhoneApplicationPage
    {
        public PopUpUserControl()
        {
            InitializeComponent();
        }

        public void CreateANewFile(object sender, EventArgs e)
        {
            if (fileNameL.Text == ""){
                return;
            }
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (store.GetDirectoryNames().Contains("./"))
            {
                System.Diagnostics.Debug.WriteLine("Writing fake files");
                string directory = "./";
                var isoFileStream = new IsolatedStorageFileStream(directory + "\\" + fileNameL.Text, FileMode.OpenOrCreate, store);
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
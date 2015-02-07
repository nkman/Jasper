using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO.IsolatedStorage;

namespace Jasper
{
    public partial class DocListing : PhoneApplicationPage
    {
        public DocListing()
        {
            InitializeComponent();
            Grid DocumentGrid = this.ContentPanel;
            AddToGrid(DocumentGrid);
        }

        public async Task createFolder()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            System.Diagnostics.Debug.WriteLine("Creating folder");
            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("Jasper", CreationCollisionOption.OpenIfExists);
        }

        public async Task<String[]> ReadFile()
        {
            System.Diagnostics.Debug.WriteLine("Reading file");
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            if (file.GetDirectoryNames().Contains("Jasper"))
            {
                string directory = "Jasper/";
                string[] filenames = file.GetFileNames(directory);
                return filenames;
            }
            else
            {
                await createFolder();
                string directory = "Jasper/";
                string[] filenames = file.GetFileNames(directory);
                return filenames;

            }
        }

        public async void AddToGrid(Grid DocumentGrid)
        {
            String[] filenames = await ReadFile();
            System.Diagnostics.Debug.WriteLine("Starting dispatcher");
            Dispatcher.BeginInvoke(() =>
            {

                StackPanel panel = new StackPanel();
                panel.Orientation = System.Windows.Controls.Orientation.Vertical;
                for (int i = 0; i < filenames.Length; i++)
                {
                    TextBlock text = new TextBlock() { Text = filenames[i] };
                    panel.Children.Add(text);
                }

                DocumentGrid.Children.Add(panel);
            });

        }

    }
}
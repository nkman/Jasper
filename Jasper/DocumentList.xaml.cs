using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace Jasper
{
    public partial class DocumentList : PhoneApplicationPage
    {

        Grid DocumentGrid;
        public DocumentList()
        {
            InitializeComponent();
            DocumentGrid = this.MyContentPanel;
            AddToGrid();
        }

        public async Task createFolder()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("Jasper", CreationCollisionOption.OpenIfExists);
        }

        public async Task<String[]> ReadFile()
        {
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

        public async void AddToGrid()
        {
            String[] filenames = await ReadFile();
            
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
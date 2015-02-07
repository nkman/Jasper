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
using System.IO;
using System.Collections.ObjectModel;

namespace Jasper
{
    public partial class DocListing : PhoneApplicationPage
    {
        ObservableCollection<DocumentList> dataSource;

        public DocListing()
        {

            InitializeComponent();
            dataSource = new ObservableCollection<DocumentList>();
            DocumentList.ItemsSource = dataSource;
            AddToGrid();
        }

        public void createFolder()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            System.Diagnostics.Debug.WriteLine("Creating folder");
            store.CreateDirectory("Jasper");

        }

        public String[] ReadFile()
        {
            System.Diagnostics.Debug.WriteLine("Reading file");

            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            if (file.GetDirectoryNames().Contains("Jasper"))
            {
                WriteToFile();
                System.Diagnostics.Debug.WriteLine("Directory found");
                string directory = "Jasper/";
                string[] filenames = file.GetFileNames(directory);
                return filenames;
            }
            else
            {

                System.Diagnostics.Debug.WriteLine("Directory Not found");
                createFolder();
                WriteToFile();
                string directory = "Jasper/";
                string[] filenames = file.GetFileNames(directory);
                return filenames;

            }
        }

        public void AddToGrid()
        {
            String[] filenames = ReadFile();

            System.Diagnostics.Debug.WriteLine("Files found: " + filenames.Length);
            System.Diagnostics.Debug.WriteLine("Creating DataSource");

            for (int i = 0; i < filenames.Length; i++)
            {
                dataSource.Add(new DocumentList(filenames[i]));
            }

            System.Diagnostics.Debug.WriteLine("Done DataSource");

        }

        private void WriteToFile()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            System.Diagnostics.Debug.WriteLine("Finding fake files");
            if (store.GetDirectoryNames().Contains("Jasper"))
            {
                System.Diagnostics.Debug.WriteLine("Writing fake files");
                string directory = "Jasper";
                var isoFileStream = new IsolatedStorageFileStream(directory + "\\dd.txt", FileMode.OpenOrCreate, store);
                isoFileStream = new IsolatedStorageFileStream(directory + "\\dd1.txt", FileMode.OpenOrCreate, store);
                isoFileStream = new IsolatedStorageFileStream(directory + "\\dd2.txt", FileMode.OpenOrCreate, store);
                isoFileStream = new IsolatedStorageFileStream(directory + "\\dd4.txt", FileMode.OpenOrCreate, store);
                isoFileStream = new IsolatedStorageFileStream(directory + "\\dd3.txt", FileMode.OpenOrCreate, store);
            }
            System.Diagnostics.Debug.WriteLine("Done Writing fake files");
        }

    }

    public class DocumentList
    {
        public string DocumentName
        {
            get;
            set;
        }

        public DocumentList(string documentName)
        {
            this.DocumentName = documentName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.IO;

namespace Jasper
{
    public partial class Note : PhoneApplicationPage
    {
        public Note()
        {
            InitializeComponent();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs =
                   isf.OpenFile("file.txt", FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(isfs))
                    {
                        Data_Baby.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }

            }
        }



        public void save_local(object sender, EventArgs e)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = isf.OpenFile("file.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        sw.WriteLine(Data_Baby.Text.ToString());
                        sw.Close();
                    }
                }

            }
            MessageBoxResult mbr = MessageBox.Show("File has been saved in local directory.", "Jasper", MessageBoxButton.OK);
        }

        public void share(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Invitation";
            emailComposeTask.Body = Data_Baby.Text;
            emailComposeTask.To = "recipient@example.com";
            emailComposeTask.Show();
        }

        public void credit(object sender, EventArgs e)
        {

        }

        public void add_invite(object sender, EventArgs e)
        {

        }

    }
}
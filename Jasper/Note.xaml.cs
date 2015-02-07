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
        StreamData sd = null;
        public static String original="TextBox";
        public string fileNameToEdit { get; set; }
        public Note()
        {
            sd = new StreamData();
            sd.openWebSocket();
            InitializeComponent();
            /**/
            System.Diagnostics.Debug.WriteLine("Lololo");
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            fileNameToEdit = NavigationContext.QueryString["file"];
            System.Diagnostics.Debug.WriteLine(fileNameToEdit);
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs =
                       isf.OpenFile(fileNameToEdit, FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(isfs))
                    {
                        textbox.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
        }




        public void save_local(object sender, EventArgs e)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fname = fileNameToEdit;
                using (IsolatedStorageFileStream isfs = isf.OpenFile(fname, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        sw.WriteLine(textbox.Text.ToString());
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
            emailComposeTask.Body = textbox.Text;
            emailComposeTask.To = "recipient@example.com";
            emailComposeTask.Show();
        }

        public void credit(object sender, EventArgs e)
        {

        }

        public void add_invite(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/invitation.xaml", UriKind.RelativeOrAbsolute));
        }

        public void Update(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(textbox.Text);
            //sd.send_string(textbox.Text);
            var dmp = new DiffMatchPatch.diff_match_patch();
            var d = dmp.diff_main(original, textbox.Text);
            //dmp.diff_cleanupSemantic(d);
            //String diffs = dmp.diff_prettyHtml(d);
            var patches = dmp.patch_make(d);
            var patch_string = dmp.patch_toText(patches);
            sd.send_string(patch_string);
            original = textbox.Text;

        }

    }
}
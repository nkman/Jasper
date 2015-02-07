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

namespace Jasper
{
    public partial class Note : PhoneApplicationPage
    {
        public Note()
        {
            InitializeComponent();
        }



        public void save_local(object sender, EventArgs e)
        {
            
        }

        public void share(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Invitation";
            emailComposeTask.Body = Data_Baby.Text;
            emailComposeTask.To = "recipient@example.com";
            //emailComposeTask.Cc = "cc@example.com";
            //emailComposeTask.Bcc = "bcc@example.com";

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Jasper
{
    public partial class Note : PhoneApplicationPage
    {
        StreamData sd = null;
        public static String original="TextBox";
        public Note()
        {
            sd = new StreamData();
            sd.openWebSocket();
            InitializeComponent();
        }



        public void save_local(object sender, EventArgs e)
        {
            
        }

        public void share(object sender, EventArgs e)
        {

        }

        public void credit(object sender, EventArgs e)
        {

        }

        public void add_invite(object sender, EventArgs e)
        {

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasper
{
    class urlConfig
    {
        public static string homeUrl()
        {
            return "http://jasperx.cloudapp.net/";
        }

        public static string loginUrl()
        {
            return homeUrl() + "login.php";
        }

        public static string signupUrl()
        {
            return homeUrl() + "signup.php";
        }

        public static string createNoteUrl()
        {
            return homeUrl() + "createDoc.php";
        }

        public static string getAllNoteUrl()
        {
            return homeUrl() + "listDoc.php";
        }
    }
}

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
            return "http://google.com/";
        }

        public static string loginUrl()
        {
            return homeUrl() + "login/";
        }

        public static string signupUrl()
        {
            return homeUrl() + "signup/";
        }
    }
}

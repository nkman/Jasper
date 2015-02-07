using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasper
{
    class urlConfig
    {
        public string homeUrl()
        {
            return "Kakul will give this/";
        }

        public string loginUrl()
        {
            return homeUrl() + "login/";
        }

        public string signupUrl()
        {
            return homeUrl() + "signup/";
        }
    }
}

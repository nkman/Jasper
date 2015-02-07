using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;

namespace Jasper
{
   public class Login
    {
       public String email, password;
       private String secretKey;
       Login()
       {
           email = null;
           password = null;
           secretKey = null;
       }

       /* The login function
        * sets secret key obtained from server
        * returns error code, 0 if success
        */
       int doLogin()
       {
           if(checkEmail()&&checkPassword())
           {
               //Send a HTTP request here
               
               return 0;
           }
           else
           {
               if (!checkEmail())
               {
                   return 1;
               }
               else return 2;
           }
       }

       bool checkEmail()
       {
           if (email.Contains('@') && email.Contains('.'))
               return true;
           else return false;
       }

       bool checkPassword()
       {
           if (password.Length < 6) return false;
           else return true;
       }
    }
}

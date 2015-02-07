﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
namespace Jasper
{
   public class Login
    {
       public String email, password;
       private String secretKey;
       public delegate void RESTSuccessCallback(Stream stream);
       public delegate void RESTErrorCallback(String reason);

       public static User user;
       public Login()
       {
           email = null;
           password = null;
           secretKey = null;
           user = null;
       }

       /* The login function
        * sets secret key obtained from server
        * returns error code, 0 if success
        */
       public int doLogin()
       {
           if(checkEmail()&&checkPassword())
           {
               //Send a HTTP request here
               string url = urlConfig.loginUrl();
               var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("password", password)
                    };
               post(new Uri(url), values);
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



       public async void post(Uri uri, List<KeyValuePair<string, string>> values)
       {
           var httpClient = new HttpClient(new HttpClientHandler());
           urlConfig urlconfig = new urlConfig();
           HttpResponseMessage response = await httpClient.PostAsync(uri, new FormUrlEncodedContent(values));
           //response.EnsureSuccessStatusCode();
           var responseString = await response.Content.ReadAsStringAsync();
           secretKey = responseString;
           System.Diagnostics.Debug.WriteLine("Post success:"+responseString);
                                                    

       }
    }
}

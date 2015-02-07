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
       public delegate void RESTSuccessCallback(Stream stream);
       public delegate void RESTErrorCallback(String reason);

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
               string url = "http://kakulkaserver";
               Dictionary<String, String> post_params=new Dictionary<string,string>();
               post_params.Add("email",email);
               post_params.Add("password",password);
               post(new Uri(url), post_params, null, success_callback, error_callback);
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

       void success_callback(Stream response_stream)
       {
           byte[] byteArray=new byte[32];
           response_stream.BeginRead(byteArray,0,32,null,null);
           secretKey = byteArray.ToString();

       }

       void error_callback(String reason)
       {

       }
       public void post(Uri uri, Dictionary<String, String> post_params, Dictionary<String, String> extra_headers, RESTSuccessCallback success_callback, RESTErrorCallback error_callback)
       {
           HttpWebRequest request = WebRequest.CreateHttp(uri);
           request.ContentType = "application/x-www-form-urlencoded";
           request.Method = "POST";
           if (extra_headers != null)
               foreach (String header in extra_headers.Keys)
                   try
                   {
                       request.Headers[header] = extra_headers[header];
                   }
                   catch (Exception) { }


           //we first obtain an input stream to which to write the body of the HTTP POST
           request.BeginGetRequestStream((IAsyncResult result) =>
           {
               HttpWebRequest preq = result.AsyncState as HttpWebRequest;
               if (preq != null)
               {
                   Stream postStream = preq.EndGetRequestStream(result);

                   //allow for dynamic spec of post body
                   StringBuilder postParamBuilder = new StringBuilder();
                   if (post_params != null)
                       foreach (String key in post_params.Keys)
                           postParamBuilder.Append(String.Format("{0}={1}&", key, post_params[key]));

                   Byte[] byteArray = Encoding.UTF8.GetBytes(postParamBuilder.ToString());

                   //guess one could just accept a byte[] [via function argument] for arbitrary data types - images, audio,...
                   postStream.Write(byteArray, 0, byteArray.Length);
                   postStream.Close();


                   //we can then finalize the request...
                   preq.BeginGetResponse((IAsyncResult final_result) =>
                   {
                       HttpWebRequest req = final_result.AsyncState as HttpWebRequest;
                       if (req != null)
                       {
                           try
                           {
                               //we call the success callback as long as we get a response stream
                               WebResponse response = req.EndGetResponse(final_result);
                               success_callback(response.GetResponseStream());
                               return;
                           }
                           catch (WebException e)
                           {
                               //otherwise call the error/failure callback
                               error_callback(e.Message);
                               return;
                           }
                       }
                   }, preq);
               }
           }, request);
       }
    }
}

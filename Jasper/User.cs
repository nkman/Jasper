using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasper
{
    class User
    {
        String firstName;
        String lastName;
        String email;
        String secretKey;

        public User(String fname, String lname, String email, String secret)
        {
            firstName = fname;
            lastName = lname;
            this.email = email;
            secretKey = secret;
        }
        //Returns secret key
        public String getSecretKey()
        {
            return secretKey;
        }

        public String getFirstName()
        {
            return firstName;
        }

        public String getLastName()
        {
            return lastName;
        }

    }
}

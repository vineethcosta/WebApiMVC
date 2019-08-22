using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;


namespace BusinessLayer
{
    public class LoginClass
    {
        BankEntities2 dbContext = new BankEntities2();

        public string Decrypt(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public string checkCredentials(string userId, string password)
        {
            string role;
            try
            {
                
                Login user = dbContext.Logins.Where(Val => (Val.userId).Equals(userId)).Single<Login>();
                string Dpassword=Decrypt(user.password);
                if (password == Dpassword)
                {
                    role = user.role;
                    return role;
                }
                else
                    return "Entered UserId or Password are Incorrect";
            }
            catch (Exception e)
            {
                return "Entered UserId or Password are Incorrect";
            }

        }

    }
}

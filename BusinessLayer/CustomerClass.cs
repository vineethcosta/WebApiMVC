using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class CustomerClass
    {
        String ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        BankEntities2 dbContext = new BankEntities2();

        public string checkMedal(long acc)
        {
            BankEntities2 dbContext2 = new BankEntities2();
            int amt;
            CustomerClass obj = new CustomerClass();

            Account account = dbContext.Accounts.Where(val => val.accountNo == acc).Single<Account>();
            amt = (int)(account.amount);
            CustomerMedal medal = dbContext2.CustomerMedals.Where(val => val.min < amt && val.max > amt).Single<CustomerMedal>();
            account.type = medal.type;
            dbContext.SaveChanges();

            return account.type;
        }

        public bool checkAccount(long acc1)
        {
            try
            {
                Account acc = dbContext.Accounts.Where(val => val.accountNo == acc1).Single<Account>();
            }
            catch (Exception exe)
            {
                return false;
            }
            return true;
        }

        public int getAmount(long acc1)
        {
            try
            {
                Account account = dbContext.Accounts.Where(val => val.accountNo == acc1).Single<Account>();
                int amt = (int)(account.amount);
                return amt;
            }
            catch (Exception e)
            {
                return -100;
            }
        }

        public void transferAdd(int amount, long acc)
        {
            try
            {
                Account account = dbContext.Accounts.Where(val => val.accountNo == acc).Single<Account>();
                account.amount += amount;
                dbContext.SaveChanges();
            }
            catch (Exception exe)
            {

            }
        }

        public void transferSub(int amount, long acc)
        {
            try
            {
                Account account = dbContext.Accounts.Where(val => val.accountNo == acc).Single<Account>();
                account.amount -= amount;
                dbContext.SaveChanges();
            }
            catch (Exception exe)
            {

            }
        }
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

        public string Encrypt(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public string changePassword(string old, string new1, string new2, string uid, out bool success)
        {
            try
            {
                Login login = dbContext.Logins.Where(val => val.userId == uid).Single<Login>();
                if (old == Decrypt(login.password))
                {
                    if (new1 == new2)
                    {
                        login.password = Encrypt(new1);
                        dbContext.SaveChanges();
                        success = true;
                        return "password Changed";

                    }
                    else
                    {
                        success = false;
                        return "Password Mismatch";

                    }
                }
                else
                {
                    success = false;
                    return "Please enter correct old password";

                }

            }
            catch (Exception exe)
            {
                success = false;
                return "User not found";
            }
        }

   
    
public List<Transaction> customstatement(int acc, DateTime start, DateTime end)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            string sql = "datecheck";
            SqlCommand command = new SqlCommand(sql, con);
            SqlParameter param1 = new SqlParameter("@acc", acc);
            command.Parameters.Add(param1);
            DateTime Start = start.Date;
            SqlParameter param2 = new SqlParameter("@start", Start.ToString());
            command.Parameters.Add(param2);
            DateTime End = end.Date;
            SqlParameter param3 = new SqlParameter("@end", End.ToString());
            command.Parameters.Add(param3);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);


            con.Close();
            int noofrows = ds.Tables[0].Rows.Count;
            int i = 0;
            List<Transaction> tlist = new List<Transaction>();
            while (noofrows != 0)
            {
                Transaction obj = new Transaction();
                noofrows--;
                obj.transactionId = long.Parse(ds.Tables[0].Rows[i]["transactionId"].ToString());
                obj.fromAccountNo = long.Parse(ds.Tables[0].Rows[i]["fromAccountNo"].ToString());
                obj.toAccountNo = long.Parse(ds.Tables[0].Rows[i]["toAccountNo"].ToString());
                obj.transactionDate = ds.Tables[0].Rows[i]["transactionDate"].ToString();
                obj.amount = int.Parse(ds.Tables[0].Rows[i]["amount"].ToString());
                obj.transactionType = ds.Tables[0].Rows[i]["transactiontype"].ToString();
                obj.comments = ds.Tables[0].Rows[i]["comments"].ToString();
                tlist.Add(obj);
                i++;
            }
            return tlist;


}
    }
}

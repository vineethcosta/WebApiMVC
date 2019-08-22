using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class ManagerClass
    {
        String ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        BankEntities2 dbContext = new BankEntities2();
        public string getBranchIdOfManager(string userId)
        {
            try
            {
                Manager manager = dbContext.Managers.Where(val => val.userId == userId).Single<Manager>();
                return manager.branchId;
            }
            catch (Exception exe)
            {
                return "Wrong ID";
            }

        }
        public string withdraw(long acc, int amt)
        {
            string comment = "withdraw done";
            string type = "withdraw";
            try
            {
                Account transactionWithdraw = dbContext.Accounts.Where(var => var.accountNo == acc).Single<Account>();
                if (transactionWithdraw.amount >= amt)
                {
                    transactionWithdraw.amount -= amt;
                    dbContext.SaveChanges();
                }
                else
                    return "amountNotSufficient";
                
            }
            catch(Exception exe)
            {
                return "accountNotFound";
            }
            TransactionClass obj1 = new TransactionClass();
            obj1.insTrans(acc, acc, amt, type, comment);
            return "Success";

        }
        public string deposit(long acc, int amt)
        {
            string comment = "deposit done";
            string type = "deposit";
            try
            {
                Account transactionWithdraw = dbContext.Accounts.Where(var => var.accountNo == acc).Single<Account>();

                transactionWithdraw.amount += amt;
                dbContext.SaveChanges();

            }
            catch (Exception exe)
            {
                return "accountNotFound";
            }
                TransactionClass obj1 = new TransactionClass();
                obj1.insTrans(acc, acc, amt, type, comment);
            return "success";
        }

        public bool AddAccount(string[] EnteredDetails)
        {
            int res = -100, result = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            string sql = "checkMedal";
            SqlCommand command2 = new SqlCommand(sql, con);
            SqlParameter param3 = new SqlParameter("@amt", EnteredDetails[6]);
            command2.Parameters.Add(param3);

            command2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            String type = ds2.Tables[0].Rows[0][0].ToString();



            SqlCommand cmd1 = new SqlCommand("checkCust", con);
            cmd1.Parameters.AddWithValue("@customerId", EnteredDetails[0]);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                SqlCommand cmd = new SqlCommand("addDetails", con);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customerId", EnteredDetails[0]);
                cmd.Parameters.AddWithValue("@accountType", EnteredDetails[1]);
                cmd.Parameters.AddWithValue("@DateOfOpen", EnteredDetails[2]);
                cmd.Parameters.AddWithValue("@status", EnteredDetails[3]);
                cmd.Parameters.AddWithValue("@dateOfEdited", EnteredDetails[4]);
                cmd.Parameters.AddWithValue("@ClosingDate", EnteredDetails[5]);


                cmd.Parameters.AddWithValue("@amount", EnteredDetails[6]);
                cmd.Parameters.AddWithValue("@type", type);



                result = cmd.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("checkCust", con);





                con.Close();

            }

            else
            {
                return false;
            }
            return true;

        }


        public int addLogin(string userId, string password, string userType)
        {
            

            Login login=new Login
            {
                userId=userId,
                password=password,
                role=userType

            };
            dbContext.Logins.Add(login);
            dbContext.SaveChanges();
            return 1;

        }

        public int addCustomer(Customer customer)
        {
            Customer AddCustomer = new Customer
            {
                customerName=customer.customerName,
                gender=customer.gender,
                dob=customer.dob,
                state=customer.state,
                address=customer.address,
                city=customer.city,
                pincode=customer.pincode,
                phoneNo = customer.phoneNo,
                email = customer.email,
                createdDate = customer.createdDate,
                editedDate = customer.editedDate,
                userId = customer.userId,
                branchId = customer.branchId,
                managerId = customer.managerId
            };
            dbContext.Customers.Add(AddCustomer);
            dbContext.SaveChanges();
            return 1;
        }

        public int updateCustomer(Customer customer)
        {
            //SqlConnection conn = new SqlConnection(ConnectionString);
            //conn.Open();
            //string procedure_name = "updateCustomer";
            //SqlCommand command = new SqlCommand(procedure_name, conn);
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlParameter id = new SqlParameter("@custId", customer.customerId);
            //command.Parameters.Add(id);
            //SqlParameter _name = new SqlParameter("@custName", customer.customerName);
            //command.Parameters.Add(_name);
            //SqlParameter _gender = new SqlParameter("@gender", customer.gender);
            //command.Parameters.Add(_gender);
            //SqlParameter _dob = new SqlParameter("@dob", customer.dob);
            //command.Parameters.Add(_dob);
            //SqlParameter _state = new SqlParameter("@address", customer.dob);
            //command.Parameters.Add(_state);
            //SqlParameter _address = new SqlParameter("@state", customer.state);
            //command.Parameters.Add(_address);

            //SqlParameter _city = new SqlParameter("@city", customer.city);
            //command.Parameters.Add(_city);
            //SqlParameter _pincode = new SqlParameter("@pincode", customer.pincode);
            //command.Parameters.Add(_pincode);
            //SqlParameter _phoneNo = new SqlParameter("@phoneNo", customer.phoneNo);
            //command.Parameters.Add(_phoneNo);


            //SqlParameter _editedDate = new SqlParameter("@editedDate", customer.editedDate);
            //command.Parameters.Add(_editedDate);

            //int rows_affected = command.ExecuteNonQuery();
            //conn.Close();
            //return rows_affected;

            Customer UpdateCustomer = dbContext.Customers.Where(val => val.customerId == customer.customerId).Single<Customer>();
            UpdateCustomer.customerName = customer.customerName;
            UpdateCustomer.gender = customer.gender;
            UpdateCustomer.dob = customer.dob;
            UpdateCustomer.address = customer.address;
            UpdateCustomer.state = customer.state;
            UpdateCustomer.city = customer.city;
            UpdateCustomer.pincode = customer.pincode;
            UpdateCustomer.phoneNo = customer.phoneNo;
            UpdateCustomer.editedDate = customer.editedDate;
            dbContext.SaveChanges();
            return 1;
        }


        public int deleteCustomer(int custId)
        {
            //SqlConnection conn = new SqlConnection(ConnectionString);
            //conn.Open();
            //string procedure_name = "deleteCustomer";
            //SqlCommand command = new SqlCommand(procedure_name, conn);
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlParameter id = new SqlParameter("@custId", custId);
            //command.Parameters.Add(id);
            //int rows_affected = command.ExecuteNonQuery();
            //conn.Close();
            //return rows_affected;
            Customer customer = dbContext.Customers.Where(val => val.customerId == custId).Single<Customer>();
            Login login = dbContext.Logins.Where(val => val.userId == customer.email).Single<Login>();
            dbContext.Logins.Remove(login);
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            return 1;
        }

        public BusinessLayer.Customer getSpecificCustomer(int custId)
        {

            //SqlConnection conn = new SqlConnection(ConnectionString);
            //conn.Open();
            ////string procedure_name = "getSpecificCustomer";
            //SqlCommand command = new SqlCommand("select * from Customer where customerId = " + custId, conn);

            //SqlDataAdapter reader = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            //BusinessLayer.Customer cobj = new BusinessLayer.Customer();
            //reader.Fill(ds);
            //conn.Close();
            //List<BusinessLayer.Customer> clist = new List<BusinessLayer.Customer>();
            //int noofrows = ds.Tables[0].Rows.Count;

            //int i = 0;
            //while (noofrows != 0)
            //{
            //    BusinessLayer.Customer obj = new BusinessLayer.Customer();
            //    noofrows--;

            //    obj.customerId = int.Parse(ds.Tables[0].Rows[i]["CustomerID"].ToString());
            //    obj.customerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
            //    obj.gender = ds.Tables[0].Rows[i]["Gender"].ToString().ElementAt(0).ToString();
            //    obj.dob = ds.Tables[0].Rows[i]["Dob"].ToString();
            //    obj.address = (ds.Tables[0].Rows[i]["Address"].ToString());
            //    obj.state = ds.Tables[0].Rows[i]["State"].ToString();
            //    obj.pincode = ds.Tables[0].Rows[i]["Pincode"].ToString();
            //    obj.phoneNo = ds.Tables[0].Rows[i]["PhoneNo"].ToString();
            //    obj.email = ds.Tables[0].Rows[i]["Email"].ToString();
            //    obj.createdDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString();
            //    obj.editedDate = ds.Tables[0].Rows[i]["EditedDate"].ToString();
            //    obj.userId = ds.Tables[0].Rows[i]["UserID"].ToString();
            //    clist.Add(obj);
            //    i++;
            //}
            //return clist;
            try
            {
                Customer customer = dbContext.Customers.Where(val => val.customerId == custId).Single<Customer>();
                return customer;
            }

            catch(Exception exe)
            {
                return null;
            }



        }

        public bool EditAccount(Account selectedAccount)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            string sql = "checkMedal";
            SqlCommand command2 = new SqlCommand(sql, con);
            SqlParameter param3 = new SqlParameter("@amt", selectedAccount.amount);
            command2.Parameters.Add(param3);

            command2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            String type = ds2.Tables[0].Rows[0][0].ToString();

            SqlCommand cmd = new SqlCommand("editedDetails", con);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accountNo", selectedAccount.accountNo);

            cmd.Parameters.AddWithValue("@accountType", selectedAccount.accountType);

            cmd.Parameters.AddWithValue("@status", selectedAccount.status);
            cmd.Parameters.AddWithValue("@dateOfEdited", selectedAccount.dateOfEdited);
            cmd.Parameters.AddWithValue("@ClosingDate", selectedAccount.ClosingDate);
            cmd.Parameters.AddWithValue("@amount", selectedAccount.amount);
            cmd.Parameters.AddWithValue("@type", type);



            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
          


        }
        public bool DeleteAccount(long accountNoToDelete)
        {
            //    SqlConnection con = new SqlConnection(ConnectionString);

            //    SqlCommand cmd = new SqlCommand("deleteDetails", con);

            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@accountNo", accountNoToDelete);

            //    con.Open();
            //    int result = cmd.ExecuteNonQuery();
            //    con.Close();

            //    if (result == 0)
            //    {
            //        return false;
            //    }

            //    else
            //    {
            //        return true;
            //    }
            //}
            Account account = dbContext.Accounts.Where(val => val.accountNo == accountNoToDelete).Single<Account>();
            dbContext.Accounts.Remove(account);
            dbContext.SaveChanges();
            return true;

        }
        }
}
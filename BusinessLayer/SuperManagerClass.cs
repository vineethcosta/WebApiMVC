using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class SuperManagerClass
    {
        BankEntities2 dbContext = new BankEntities2();
        String ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        public string addManager(string name, string branch, string address, string phno, string mail)
        {
            //SqlConnection con = new SqlConnection(ConnectionString);
            //con.Open();
            //string sql = "addManager";

            //SqlCommand command = new SqlCommand(sql, con);
            //SqlParameter param1 = new SqlParameter("@managerName", name);


            //command.Parameters.Add(param1);
            //SqlParameter param2 = new SqlParameter("@branchId", branch);


            //command.Parameters.Add(param2);

            //SqlParameter param3 = new SqlParameter("@address", address);


            //command.Parameters.Add(param3);
            //SqlParameter param4 = new SqlParameter("@phoneNo", phno);


            //command.Parameters.Add(param4);
            //SqlParameter param5 = new SqlParameter("@email", mail);


            //command.Parameters.Add(param5);

            //command.CommandType = CommandType.StoredProcedure;
            //command.ExecuteNonQuery();
            //return "success";

            Manager addManager = new Manager
                 {
                     managerName = name,
                     branchId = branch,
                     address = address,
                     phoneNo = phno,
                     userId = mail
                 };

            return "success";
        }
        public List<String> getNonAssignedBranches()
        {
            //SqlConnection con = new SqlConnection(ConnectionString);
            //con.Open();
            //string sql = "getNonAssignedBranches";

            //SqlCommand command = new SqlCommand(sql, con);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlConnection conn = new SqlConnection(ConnectionString);
            //conn.Open();
            //SqlCommand command = new SqlCommand("select branchId from Branch where assigned = 0", conn);
            //SqlDataAdapter reader = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            //reader.Fill(ds);
            //conn.Close();
            //List<string> branchesList = new List<string>();
            //int noOfRows = ds.Tables[0].Rows.Count;
            //int i = 0;
            //while (noOfRows-- != 0)
            //{
            //    branchesList.Add(ds.Tables[0].Rows[i]["branchId"].ToString());
            //    i++;
            //}
            List<Branch> nonAssignedBranchList = dbContext.Branches.Where(val => val.assigned == 0).ToList<Branch>();
            List<String> branchIdList = new List<String>();
            foreach(Branch s in nonAssignedBranchList)
            {
                branchIdList.Add(s.branchId);
            }
            return branchIdList;
        }

        public string editManager(int mId, string name, string address, string phno, string mail, string branch)
        {
            //SqlConnection con = new SqlConnection(ConnectionString);
            //con.Open();
            //string sql = "editManager";

            //SqlCommand command = new SqlCommand(sql, con);

            //SqlParameter param6 = new SqlParameter("@managerId", mId);
            //command.Parameters.Add(param6);

            //SqlParameter param1 = new SqlParameter("@managerName", name);
            //command.Parameters.Add(param1);

            //SqlParameter param3 = new SqlParameter("@address", address);
            //command.Parameters.Add(param3);

            //SqlParameter param4 = new SqlParameter("@phoneNo", phno);
            //command.Parameters.Add(param4);

            //SqlParameter param5 = new SqlParameter("@emailId", mail);
            //command.Parameters.Add(param5);

            //SqlParameter param2 = new SqlParameter("@branchId", branch);
            //command.Parameters.Add(param2);

           

            //command.CommandType = CommandType.StoredProcedure;
            //command.ExecuteNonQuery();
            //return "success";
            Manager editManager = dbContext.Managers.Where(val => val.managerId == mId).Single<Manager>();
            editManager.managerName = name;
            editManager.address = address;
            editManager.phoneNo = phno;
            editManager.userId = mail;
            editManager.branchId = branch;
            Branch addAssigned = dbContext.Branches.Where(val => val.branchId == branch).Single<Branch>();
            addAssigned.assigned = 1;
            dbContext.SaveChanges();
            
           
            return "success";
            
        }

        public string deleteManager(int mId)
        {
            //SqlConnection con = new SqlConnection(ConnectionString);
            //con.Open();
            //string sql = "deleteManager";

            //SqlCommand command = new SqlCommand(sql, con);
            
            //SqlParameter param = new SqlParameter("@managerId", mId);
            //command.Parameters.Add(param);

            //command.CommandType = CommandType.StoredProcedure;
            //command.ExecuteNonQuery();
            Manager deleteManager = dbContext.Managers.Where(val => val.managerId == mId).Single<Manager>();
            dbContext.Managers.Remove(deleteManager);
            dbContext.SaveChanges();

            
            return "success";
        }

        public string assignToZero(int mId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            string sql = "setAssignedTo0";
            SqlCommand command = new SqlCommand(sql, con);

            SqlParameter param = new SqlParameter("@managerId", mId);
            command.Parameters.Add(param);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();

            return "success";
        }
        
    }
}

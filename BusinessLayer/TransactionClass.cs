using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace BusinessLayer
{
    public class TransactionClass
    {


        public void insTrans(long acc1, long acc2, int amt, string type, string comment)
        {
            BankEntities2 dbContext=new BankEntities2();
            string date = DateTime.Now.ToString();
            Transaction transactionInsert = new Transaction
            {
                fromAccountNo = acc1,
                toAccountNo = acc2,
                transactionDate=date,
                amount = amt,
                transactionType = type,
                comments = comment
            };
          dbContext.Transactions.Add(transactionInsert);
          dbContext.SaveChanges();

        }






    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NaijaBank
{
    class Bank
    {
        //protected string bankName;
        public static List<Customer> allCustomers = new List<Customer>();

        //method to fetch all bank customers
        public static string GetAllCustomers()
        {
           if(allCustomers.Count > 0)
            {
                var BankCustomers = new StringBuilder();
                BankCustomers.AppendLine("Customer Name\t\tCustomer Email");
                foreach (var customer in allCustomers)
                {
                    BankCustomers.AppendLine($"{customer.FullName}\t\t{customer.Email}\t\t{customer.Username}");
                }
                return BankCustomers.ToString();
            }
            return "You have no customers in your bank presently";
        }
    }

   
}

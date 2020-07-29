using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NaijaBank
{
    class Program
    {
        static void Main(string[] args)
        {

            ProgramIntro();
        }
        static void ProgramIntro()
        {
        ///BEGINNING OF APPLICATION

        MainMenu: Console.WriteLine("Welcome to NaijaBank..");
            Console.WriteLine("If you are a new customer, type 1 to register. If you are an existing customer type 2 to login");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            var response = Console.ReadLine();

            ///WHAT TO DO IF USER SELECTS OPTION TO CREATE ACCOUNT
            #region Retrieve User Info
            if (response == 1.ToString())
            {
                Console.WriteLine("Answer the following questions to create account");
                Console.Write("First name: ");
                var firstName = Console.ReadLine();
                Console.Write("Last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Email: ");
                var email = Console.ReadLine();
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                #endregion
                ///CHECK IF ALL REQUIRED VALUES TO CREATE CUSTOMER HAVE BEEN PROVIDED
            #region Create Bank Account
                if (firstName.Length == 0 || lastName.Length == 0 || email.Length == 0 || username.Length == 0 || password.Length == 0)
                {
                    Console.WriteLine("Sorry, we cannot create an account for you as you did not provide all your details");
                }
                else
                {
                    Customer newCustomer = new Customer(firstName, lastName, email, username, password);
                    Console.WriteLine("Congratulations, you have been registered as a customer at NaijaBank. Return to the Main Menu to Login");
                    Console.WriteLine("Press any key to return to the Main Menu");
                    Console.ReadLine();
                    goto MainMenu;
                }
            }
            /// WHAT TO DO IF USER SELECTS OPTION TO LOGIN
            else if (response == 2.ToString())
            {
                LogIn(Bank.allCustomers);
            }
            else Console.WriteLine("Invalid response. Press any key to continue");
            Console.ReadLine();
            goto MainMenu;
        }
        //PROGRAM FLOW AFTER SUCCESSFULLY CREATING ACCOUNT
       static void LogIn(List<Customer> customers)
        {
            //Customer newCustomer;
            Login:  Console.WriteLine("Enter your login details to perform account transactions");
            Console.Write("Enter Username: ");
            var Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            var Password = Console.ReadLine();
            foreach (var customer in customers)
            {
                if (customer.Username == Username && customer.Password == Password)
                {
                    customer.LogIn(Username, Password);
                    // PROGRAM FLOW AFTER SUCCESSFUL LOGIN
                    #region LoggedInRegion
                    Console.WriteLine($"Welcome, you are successfully logged in as {customer.FullName}");
                //Console.WriteLine("Which of your accounts would you like to transact on today?");
                LoginMenu: Console.WriteLine("What would you like to do? Enter the number that corresponds to your choice");
                    Console.WriteLine("1. Create Account");
                    Console.WriteLine("2. Select Account");
                    var choice = Console.ReadLine();
                    switch (int.Parse(choice)) 
                    {
                        case 1:
                        /// GET DETAILS NEEDED TO CREATE ACCOUNT
                        AccountChoice: Console.WriteLine("Select Account Type (Type the corresponding number of your selection)");
                            Console.WriteLine("1. Savings - Minimum deposit required is N100");
                            Console.WriteLine("2. Current - Minimum deposit required is N1000");
                            var accountType = Console.ReadLine();
                            string type;
                            if (accountType == 1.ToString())
                            {

                                Console.WriteLine("How much do you want to deposit?");
                                var amount = decimal.Parse(Console.ReadLine());
                                while (amount < 100)
                                {
                                    Console.WriteLine("Minimum deposit required for a savings account is N100");
                                    Console.Write("Enter deposit amount: ");
                                    amount = decimal.Parse(Console.ReadLine());
                                }
                                type = "savings";
                                BankAccount customerAccount = new BankAccount(customer, type, amount);
                                Console.WriteLine(customerAccount.Note);
                                Console.WriteLine("Press any key to return to the account menu to perform transactions");
                                Console.ReadLine();
                                goto LoginMenu;
                                //LogIn();

                            }
                            else if (accountType == 2.ToString())
                            {
                                Console.WriteLine("How much do you want to deposit?");
                                var amount = decimal.Parse(Console.ReadLine());
                                while (amount < 1000)
                                {
                                    Console.WriteLine("Minimum deposit required for a current account is N1000");
                                    Console.Write("Enter deposit amount: ");
                                    amount = decimal.Parse(Console.ReadLine());
                                }
                                type = "current";
                                BankAccount customerAccount = new BankAccount(customer, type, amount);
                                Console.WriteLine(customerAccount.Note);
                                Console.WriteLine("Press any key to return to the account menu to perform transactions");
                                Console.ReadLine();
                                goto LoginMenu;
                                //LogIn();
                            }
                            else Console.WriteLine("Invalid selection. Press any key to try again");
                            Console.ReadLine();
                            goto AccountChoice;
                        case 2:
                           GetAccount: Console.WriteLine(customer.GetAccounts());
                            Console.Write("Which of your above accounts do you want to transact with? Enter Account Number: ");
                            var AccNum = Console.ReadLine();
                            BankAccount SelectedAccount = null;
                            foreach (var account in customer.myAccounts)
                            {
                                Console.WriteLine(account.AccNumber);
                                if (account.AccNumber == int.Parse(AccNum))
                                {
                                    SelectedAccount = account;
                                  
                                }
                                /*else Console.WriteLine("Invalid Account Number");
                                Console.WriteLine("Press any key to try again");
                                Console.ReadLine();
                                goto GetAccount;*/
                            }
                            if(SelectedAccount == null)
                            {
                                Console.WriteLine("Invalid Account Number");
                                Console.WriteLine("Press any key to try again");
                                Console.ReadLine();
                                goto GetAccount;
                            }
                            else
                            {
                            TransactionChoice: Console.WriteLine("What would you like to do? Enter the number that corresponds to your choice");
                                Console.WriteLine("1. Deposit");
                                Console.WriteLine("2. Withdraw");
                                Console.WriteLine("3. Get Account Balance");
                                Console.WriteLine("4. Transfer");
                                Console.WriteLine("5. Get Account Statement");
                                Console.WriteLine("6. Log Out");
                                /*Console.WriteLine("Press # key to go to account menu");
                                var response = Console.ReadLine();
                                if(response == '#'.ToString())
                                {
                                    goto LoginMenu;
                                }*/

                                var action = Console.ReadLine();
                                switch (int.Parse(action))
                                {
                                    case 1:
                                        Console.Write("Deposit amount: ");
                                        var DepositAmount = decimal.Parse(Console.ReadLine());
                                        Console.Write("Optional note for this transaction: ");
                                        var DepositNote = Console.ReadLine();
                                        SelectedAccount.MakeDeposit(DepositAmount, DateTime.Now, DepositNote);
                                        Console.Write("Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    case 2:
                                        Console.Write("Withdrawal amount: ");
                                        var WithdrawalAmount = decimal.Parse(Console.ReadLine());
                                        Console.Write("Optional note for this transaction: ");
                                        var WithdrawalNote = Console.ReadLine();
                                        SelectedAccount.MakeWithdrawal(WithdrawalAmount, DateTime.Now, WithdrawalNote);
                                        Console.Write("Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    case 3:
                                        Console.WriteLine($"Your Account Balance is {SelectedAccount.Balance}");
                                        Console.Write("Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    case 4:
                                        Console.WriteLine("case transfer");
                                        Console.Write("Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    case 5:
                                        Console.WriteLine(SelectedAccount.GetStatement());
                                        Console.Write("Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    case 6:
                                        Console.WriteLine("working");
                                        customer.LogOut();
                                        ProgramIntro();
                                        break;
                                    default:
                                        Console.WriteLine("You have made an invalid selection");
                                        break;

                                }
                                goto TransactionChoice;
                            }
                        default:
                            break;
                            #endregion
                    }

                }
                #endregion
                else Console.WriteLine("Incorrect username or password. Press any key to try again");
                Console.ReadLine();
                goto Login;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Management_System
{
    public class Print
    {
        public void printLoginPage()
        {
            Console.Clear();
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|              WELCOME TO SIMPLE BANKING SYSTEM              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                       LOGIN TO START                       |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     USER NAME:                                             |");
            Console.WriteLine("|     PASSWORD:                                              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printMainMenu()
        {
            Console.Clear();
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|              WELCOME TO SIMPLE BANKING SYSTEM              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|       1. Create a new account                              |");
            Console.WriteLine("|       2. Search for an account                             |");
            Console.WriteLine("|       3. Deposit                                           |");
            Console.WriteLine("|       4. Withdraw                                          |");
            Console.WriteLine("|       5. A/C statement                                     |");
            Console.WriteLine("|       6. Delete account                                    |");
            Console.WriteLine("|       7. Exit                                              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Enter your choice (1~7):                               |");
            Console.WriteLine("|____________________________________________________________|");

            Console.WriteLine();
        }

        public void printAccountCreation()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                    CREATE A NEW ACCOUNT                    |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     First Name:                                            |");
            Console.WriteLine("|     Last Name:                                             |");
            Console.WriteLine("|     Address:                                               |");
            Console.WriteLine("|     Phone:                                                 |");
            Console.WriteLine("|     Email:                                                 |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printSearchAccount()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     SEARCH AN ACCOUNT                      |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Account Number:                                        |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printDeposit()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                          DEPOSIT                           |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Account Number:                                        |");
            Console.WriteLine("|     Amount: $                                              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printWithdraw()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                          WITHDRAW                          |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Account Number:                                        |");
            Console.WriteLine("|     Amount: $                                              |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printAccountStatement()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                         STATEMENT                          |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Account Number:                                        |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printDeleteAccount()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     DELETE AN ACCOUNT                      |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                     ENTER THE DETAILS                      |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|     Account Number:                                        |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }
    }
}

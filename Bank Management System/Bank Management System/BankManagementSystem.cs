using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Bank_Management_System
{
    public class BankManagementSystem
    {
        public List<Account> accounts;
        public Print p = new Print();

        public BankManagementSystem()
        {
            accounts = new List<Account>();
        }

        public static void Main(string[] args)
        {
            ConsoleKey choice = 0;
            new BankManagementSystem().LoginMenu(ref choice);
        }

        public void LoginMenu(ref ConsoleKey choice)
        {
            p.printLoginPage();

            Console.SetCursorPosition(17, 7);
            string username = Console.ReadLine();

            Console.SetCursorPosition(17, 8);
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Spacebar && key.Key != ConsoleKey.Enter &&
                        key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Tab &&
                        key.Key != ConsoleKey.Delete && key.Key != ConsoleKey.Backspace &&
                        key.KeyChar != '\u0000')
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.SetCursorPosition(0, 11);

            if (File.Exists("login.txt"))
            {
                bool loginSuccess = false;
                string[] userLists = File.ReadAllLines("login.txt");
                try
                {
                    foreach (string user in userLists)
                    {
                        string[] separator = { "|", " " };
                        string[] input = user.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        if (username == input[0] && password == input[1])
                        {
                            loginSuccess = true;
                            choice = ConsoleKey.N;
                            MainMenu();
                            break;
                        }
                        else continue;
                    }
                    if (!loginSuccess)
                    {
                        Console.WriteLine(" Your username or password is incorrect!");
                        Console.Write(" Retry (Y/N)? ");
                        readOption(ref choice, ref choice, "Login");
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    choice = ConsoleKey.N;
                    Console.WriteLine(" Error: login.txt does not contain login credentials in the correct format.\n");
                    Console.Write(" Press any key to exit... ");
                    Console.ReadKey();
                }
            }
            else
            {
                choice = ConsoleKey.N;
                Console.WriteLine(" Invalid credentials!");
                Console.ReadKey();
            }
        }

        public void MainMenu()
        {
            ConsoleKey choice = 0;
            ConsoleKey choice1 = 0;
            ConsoleKey choice2 = 0;
            bool invalidInput = false;

            while (choice != ConsoleKey.D7)
            {
                Console.Clear();
                p.printMainMenu();
                if (invalidInput)
                {
                    Console.WriteLine(" Invalid input. Please try again.");
                }
                invalidInput = false;

                Console.SetCursorPosition(31, 14);
                choice = Console.ReadKey().Key;

                switch (choice)
                {
                    case ConsoleKey.D1:
                        createAccount(ref choice1, ref choice2);
                        break;

                    case ConsoleKey.D2:
                        searchAccount(ref choice1);
                        break;

                    case ConsoleKey.D3:
                        deposit(ref choice1);
                        break;

                    case ConsoleKey.D4:
                        withdraw(ref choice1);
                        break;

                    case ConsoleKey.D5:
                        viewStatement(ref choice1);
                        break;

                    case ConsoleKey.D6:
                        deleteAccount(ref choice1);
                        break;

                    case ConsoleKey.D7:
                        Console.WriteLine();
                        Console.WriteLine();
                        break;

                    default:
                        invalidInput = true;
                        break;
                }
            }
        }

        public void createAccount(ref ConsoleKey choice1, ref ConsoleKey choice2)
        {
            Console.Clear();
            p.printAccountCreation();

            char[] trimChars = { ' ' };
            Console.SetCursorPosition(18, 7);
            string firstname = Console.ReadLine().Trim(trimChars);
            Console.SetCursorPosition(18, 8);
            string lastname = Console.ReadLine().Trim(trimChars);
            Console.SetCursorPosition(18, 9);
            string address = Console.ReadLine().Trim(trimChars);
            Console.SetCursorPosition(18, 10);
            string phonenumber = Console.ReadLine().Trim(trimChars);
            Console.SetCursorPosition(18, 11);
            string email = Console.ReadLine().Trim(trimChars);
            Console.SetCursorPosition(0, 14);
            Console.Write(" Is the information correct (y/n)? ");

            do
            {
                choice1 = Console.ReadKey().Key;
                switch (choice1)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine();
                        if (Int64.TryParse(phonenumber, out long phonenumberInt) && phonenumber.Length == 10)
                        {
                            int accountNumber;
                            do
                            {
                                accountNumber = new Random().Next(100000, 99999999);
                            } while (File.Exists($"{accountNumber}.txt"));
                            Account newAccount = new Account(accountNumber, 0, firstname, lastname,
                                address, phonenumber, email, "New");

                            if (newAccount.sendEmail("AccountInfo"))
                            {
                                accounts.Add(newAccount);
                                newAccount.updateFile();
                                Console.WriteLine(" Account created! Details will be sent by email shortly.");
                                Console.WriteLine($" Account Number: {accountNumber}");
                                Console.Write("\n Create another account (Y/N)? ");
                                readOption(ref choice1, ref choice2, "Create");
                            }

                            else
                            {
                                Console.WriteLine(" Your email address is invalid!");
                                Console.Write(" Retry (Y/N)? ");
                                readOption(ref choice1, ref choice2, "Create");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Your phone number is invalid!");
                            Console.Write(" Retry (Y/N)? ");
                            readOption(ref choice1, ref choice2, "Create");
                        }
                        break;

                    case ConsoleKey.N:
                        createAccount(ref choice1, ref choice2);
                        break;

                    default:
                        Console.Write("\b \b");
                        break;
                }
            } while (choice1 != ConsoleKey.Y && choice2 != ConsoleKey.N);
        }

        public void searchAccount(ref ConsoleKey choice)
        {
            Console.Clear();
            p.printSearchAccount();

            Console.SetCursorPosition(22, 7);
            string accountNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 10);

            if (accountExists(accountNumber))
            {
                account(Convert.ToInt32(accountNumber)).printAccountDetails();
                Console.Write(" Search another account (Y/N)? ");
                readOption(ref choice, ref choice, "Search");
            }
            else
            {
                Console.WriteLine(" Account not found!");
                Console.Write(" Retry (Y/N)? ");
                readOption(ref choice, ref choice, "Search");
            }
        }

        public void deposit(ref ConsoleKey choice)
        {
            Console.Clear();
            p.printDeposit();

            Console.SetCursorPosition(22, 7);
            string accountNumber = Console.ReadLine();

            if (accountExists(accountNumber))
            {
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(" Account found! Enter the amount...");
                Console.SetCursorPosition(15, 8);
                string tempAmount = Console.ReadLine();
                Console.SetCursorPosition(0, 12);

                if (Double.TryParse(tempAmount, out double amount))
                {
                    account(Convert.ToInt32(accountNumber)).deposit(amount);
                    Console.WriteLine(" Deposit successful.");
                    Console.Write("\n Make another deposit (Y/N)? ");
                    readOption(ref choice, ref choice, "Deposit");
                }

                else
                {
                    Console.WriteLine(" Invalid amount.");
                    Console.Write(" Retry (Y/N)? ");
                    readOption(ref choice, ref choice, "Deposit");
                }
            }

            else
            {
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(" Account not found!");
                Console.Write(" Retry (Y/N)? ");
                readOption(ref choice, ref choice, "Deposit");
            }
        }

        public void withdraw(ref ConsoleKey choice)
        {
            Console.Clear();
            p.printWithdraw();

            Console.SetCursorPosition(22, 7);
            string accountNumber = Console.ReadLine();

            if (accountExists(accountNumber))
            {
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(" Account found! Enter the amount...");
                Console.SetCursorPosition(15, 8);
                string tempAmount = Console.ReadLine();
                Console.SetCursorPosition(0, 12);

                if (Double.TryParse(tempAmount, out double amount))
                {

                    if (account(Convert.ToInt32(accountNumber)).isFundSufficient(amount))
                    {
                        account(Convert.ToInt32(accountNumber)).withdraw(amount);
                        Console.WriteLine(" Withdraw successful!");
                        Console.Write("\n Make another withdrawal (Y/N)? ");
                        readOption(ref choice, ref choice, "Withdraw");
                    }

                    else
                    {
                        Console.WriteLine(" Insufficient funds.");
                        Console.Write(" Retry (Y/N)? ");
                        readOption(ref choice, ref choice, "Withdraw");
                    }
                }

                else
                {
                    Console.WriteLine(" Invalid amount.");
                    Console.Write(" Retry (Y/N)? ");
                    readOption(ref choice, ref choice, "Withdraw");
                }
            }
            else
            {
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(" Account not found!");
                Console.Write(" Retry (Y/N)? ");
                readOption(ref choice, ref choice, "Withdraw");
            }
        }

        public void viewStatement(ref ConsoleKey choice)
        {
            Console.Clear();
            p.printAccountStatement();

            Console.SetCursorPosition(22, 7);
            string accountNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 10);

            if (accountExists(accountNumber))
            {
                account(Convert.ToInt32(accountNumber)).printStatement();
                Console.Write(" Email statement (Y/N)? ");
                readOption(ref choice, ref choice, "Statement", Convert.ToInt32(accountNumber));
                Console.Write("\n View another statement (Y/N)? ");
                readOption(ref choice, ref choice, "Statement2");
            }
            else
            {
                Console.WriteLine(" Account not found!");
                Console.Write(" Retry (Y/N)? ");
                readOption(ref choice, ref choice, "Statement2");
            }
        }

        public void deleteAccount(ref ConsoleKey choice)
        {
            Console.Clear();
            p.printDeleteAccount();

            Console.SetCursorPosition(22, 7);
            string accountNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 10);

            if (accountExists(accountNumber))
            {
                account(Convert.ToInt32(accountNumber)).printAccountDetails();
                Console.Write(" Delete account (Y/N)? ");
                readOption(ref choice, ref choice, "Delete", Convert.ToInt32(accountNumber));
            }
            else
            {
                Console.WriteLine(" Account not found!");
                Console.Write(" Retry (Y/N)? ");
                readOption(ref choice, ref choice, "Delete2");
            }
        }

        public void readOption(ref ConsoleKey choice1, ref ConsoleKey choice2, string option, int accountNumber = 0)
        {
            do
            {
                choice2 = Console.ReadKey().Key;
                switch (choice2)
                {
                    case ConsoleKey.Y:
                        if (option == "Login")
                            LoginMenu(ref choice2);
                        else if (option == "Create")
                            createAccount(ref choice1, ref choice2);
                        else if (option == "Search")
                            searchAccount(ref choice2);
                        else if (option == "Deposit")
                            deposit(ref choice2);
                        else if (option == "Withdraw")
                            withdraw(ref choice2);
                        else if (option == "Statement")
                        {
                            choice2 = ConsoleKey.N;
                            account(accountNumber).sendEmail("Statement");
                            Console.WriteLine("\n Email has been sent.");
                        }
                        else if (option == "Statement2")
                            viewStatement(ref choice2);
                        else if (option == "Delete")
                        {
                            accounts.Remove(account(accountNumber));
                            File.Delete($"{accountNumber}.txt");
                            Console.WriteLine("\n Account was successfully deleted!");
                            Console.Write("\n Delete another account (Y/N)? ");
                            readOption(ref choice1, ref choice2, "Delete2");
                        }
                        else if (option == "Delete2")
                            deleteAccount(ref choice2);
                        break;

                    case ConsoleKey.N:
                        break;

                    default:
                        Console.Write("\b \b");
                        break;
                }
            } while (choice2 != ConsoleKey.N);
        }

        public bool accountExists(string accountNumber)
        {
            if (accountNumber.Length >= 6 && accountNumber.Length <= 8 &&
                Int32.TryParse(accountNumber, out int accountNumberInt) && File.Exists($"{accountNumberInt}.txt"))
            {

                if (account(accountNumberInt) == null)
                {
                    string[] accountInfo = File.ReadAllLines($"{accountNumberInt}.txt").Take(7).ToArray();

                    try
                    {
                        Account existingAccount = new Account(accountNumberInt, Convert.ToDouble(accountInfo[1]),
                                accountInfo[2], accountInfo[3], accountInfo[4], accountInfo[5], accountInfo[6], "Existing");
                        accounts.Add(existingAccount);
                        existingAccount.ReAddExistingStatement();
                    }
                    catch (Exception ex) when (ex is IndexOutOfRangeException || ex is FormatException)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public Account account(int accountNumber)
        {
            foreach (Account acnt in accounts)
            {
                if (acnt.hasNumber(accountNumber))
                    return acnt;
            }
            return null;
        }
    }
}

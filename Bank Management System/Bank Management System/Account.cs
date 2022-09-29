using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace Bank_Management_System
{
    public class Account
    {
        private int accountNumber;
        private double balance;
        private string firstName, lastName, address, phoneNumber, email;
        private List<Transaction> statements = new List<Transaction>();

        public Account(int accountNumber, double balance, string firstName, string lastName,
                       string address, string phoneNumber, string email, string accountType)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            if (accountType.Equals("New"))
            {
                statements.Add(new Transaction(DateTime.Now, "Opening Balance", 0, 0, balance));
                statements.Add(new Transaction(DateTime.Now, "Closing Balance", 0, 0, balance));
            }
        }
        public void deposit(double amount)
        {
            balance += amount;
            updateStatement(amount, "Deposit");
            updateFile();
        }
        public void withdraw(double amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                updateStatement(amount, "Withdraw");
                updateFile();
            }
        }

        public void updateFile()
        {
            string statementStringBlock = "";
            foreach (Transaction transaction in statements)
                statementStringBlock += transaction.textFileString();
            File.WriteAllText(string.Format($"{accountNumber}.txt"),
                              string.Format($"{accountNumber}\n{balance:0.00}\n{firstName}\n{lastName}\n" +
                              $"{address}\n{phoneNumber}\n{email}\n{statementStringBlock}"));
        }

        public void updateStatement(double amount, string type)
        {
            statements.RemoveAt(statements.Count - 1);
            if (type == "Deposit")
                statements.Add(new Transaction(DateTime.Now, "Deposit", 0, amount, balance));
            else if (type == "Withdraw")
                statements.Add(new Transaction(DateTime.Now, "Withdrawal", amount, 0, balance));
            statements.Add(new Transaction(DateTime.Now, "Closing Balance", 0, 0, balance));
        }

        public bool hasNumber(int accountNumber)
        {
            return this.accountNumber == accountNumber;
        }

        public bool isFundSufficient(double amount)
        {
            return balance >= amount;
        }

        public void ReAddExistingStatement()
        {
            string[] statementArray = File.ReadAllLines($"{accountNumber}.txt").Skip(7).ToArray();
            foreach (string transaction in statementArray)
            {
                string[] separator = { "," };
                string[] txnInfo = transaction.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                statements.Add(new Transaction(Convert.ToDateTime(txnInfo[0]), txnInfo[1], Convert.ToDouble(txnInfo[2]),
                        Convert.ToDouble(txnInfo[3]), Convert.ToDouble(txnInfo[4])));
            }
        }


        public bool sendEmail(string option)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("assignment13978364@gmail.com", "bnkusbxqnyvgaluy");
            client.EnableSsl = true;

            try
            {
                MailMessage mail = new MailMessage(new MailAddress("assignment13978364@gmail.com", "Simple Bank"),
                                                   new MailAddress(email));
                mail.IsBodyHtml = true;

                if (option.Equals("AccountInfo"))
                {
                    mail.Subject = "Welcome to Simple Bank";
                    mail.Body = string.Format($"Dear {firstName},<br><br>" +
                                "Welcome to Simple Bank Management System!<br><br>" +
                                "Your account details are as follow:<br>" +

                                $"Account number: {accountNumber}<br>" +
                                $"First name: {firstName}<br>" +
                                $"Last name: {lastName}<br>" +
                                $"Address: {address}<br>" +
                                $"Phone number: {phoneNumber}<br>" +
                                $"Email: {email}<br><br>" +

                                "Thank you for choosing us.<br>" +
                                "Kind Regards<br>" +
                                "The Simple Banking Team");
                }

                else if (option.Equals("Statement"))
                {
                    string statementStringBlock = "";
                    foreach (Transaction transaction in statements)
                        statementStringBlock += transaction.emailString();

                    mail.Subject = "Your Account Statement";
                    mail.Body = "<style>" +
                                    "table {border-collapse: collapse; width:1000px;}" +
                                    "td, th {text-align: left; padding: 3px;}" +
                                "</style>" +

                                string.Format($"Dear {firstName},<br><br>") +
                                "Below is your account statement:<br><br>" +

                                "<table>" +
                                    "<tr><th>Date</th><th>Description</th><th>Debit</th>" +
                                    "<th>Credit</th><th>Balance</th></tr>" +
                                    statementStringBlock +
                                "</table><br>" +

                                "Thank you for choosing us.<br>" +
                                "Kind Regards,<br>" +
                                "The Simple Banking Team";
                }

                client.Send(mail);
                return true;
            }
            catch (Exception ex) when (ex is FormatException || ex is SmtpException)
            {
                return false;
            }
        }

        public void printAccountDetails()
        {
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|                       ACCOUNT DETAILS                      |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|                                                            |");
            Console.WriteLine($"|     Account Number: {accountNumber}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     Account Balance: ${balance:0.00}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     First Name: {firstName}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     Last Name: {lastName}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     Address: {address}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     Phone: {phoneNumber}".PadRight(61, ' ') + "|");
            Console.WriteLine($"|     Email: {email}".PadRight(61, ' ') + "|");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }

        public void printStatement()
        {
            Console.WriteLine(" Date".PadRight(29, ' ') + "Description".PadRight(24, ' ') + "Debit".PadRight(16, ' ') +
                              "Credit".PadRight(16, ' ') + "Balance".PadRight(16, ' '));
            Console.WriteLine(" ".PadRight(100, '-'));
            foreach (Transaction transaction in statements)
                Console.WriteLine(" " + transaction);
            Console.WriteLine();
        }
    }
}

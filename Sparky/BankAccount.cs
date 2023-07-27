using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        public decimal Balance { get; set; }
        private readonly ILogBook logBook;

        public BankAccount(ILogBook logBook)
        {
            Balance = 0;
            this.logBook = logBook;
        }

        public bool Deposit(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                logBook.LogMessage("Deposit invoked!");
                logBook.LogMessage("Test");
                logBook.Severity = 101;
                logBook.Severity = 101;
                var temp = logBook.Severity;
                return true;
            }
            return false;
        }

        public bool Withdraw(int amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                logBook.LogMessageToDb($"Withdrew {amount:0.00}!");
                return logBook.LogBalanceAfterWithdrawal(Balance);
            }
            return logBook.LogBalanceAfterWithdrawal(Balance-amount);
        }

        public decimal GetBalance()
        {
            return Balance;
        }
    }
}

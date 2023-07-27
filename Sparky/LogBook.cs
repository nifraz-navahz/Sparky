using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogBook
    {
        public int Severity { get; set; }
        public string? Type { get; set; }

        public void LogMessage(string message);
        public bool LogMessageToDb(string message);
        public bool LogBalanceAfterWithdrawal(decimal balance);
        public string? LogMessageAndReturnString(string str);
        public bool LogWithOutputResult(string str, out string output);
        public bool LogWithRefObj(ref Customer obj); 
    }
    public class LogBook : ILogBook
    {
        public int Severity { get; set; }
        public string? Type { get; set; }

        public bool LogBalanceAfterWithdrawal(decimal balance)
        {
            if (balance >= 0)
            {
                Console.WriteLine("Success!");
                return true;
            }
            Console.WriteLine("Failure!");
            return false;
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string? LogMessageAndReturnString(string str)
        {
            Console.WriteLine(str);
            return str?.ToLower();
        }

        public bool LogMessageToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithOutputResult(string str, out string output)
        {
            output = $"Hello {str}";
            return true;
        }

        public bool LogWithRefObj(ref Customer obj)
        {
            return true;
        }
    }

    //public class FakeLogBook : ILogBook
    //{
    //    public void LogMessage(string message)
    //    {
    //    }
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Customer: ICustomer
    {
        public string? GreetMessage { get; set; }
        public int Discount { get; set; } = 15;
        public int OrderCount { get; set; }
        public bool IsPlatinum { get; set; }

        public Customer()
        {
            IsPlatinum = false;
        }
        public string CombineNamesAndGreet(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Empty First Name");
            }

            GreetMessage = $"Hello {firstName} {lastName}!";
            Discount += 20;
            return GreetMessage;
        }

        public CustomerType GetCustomerDetails()
        {
            if (OrderCount < 100)
            {
                return new BasicCustomer();
            }
            return new PlatinumCustomer();
        }
    }

    public interface ICustomer
    {
        public string? GreetMessage { get; set; }
        public int Discount { get; set; }
        public int OrderCount { get; set; }
        public bool IsPlatinum { get; set; }

        public string CombineNamesAndGreet(string firstName, string lastName);
        public CustomerType GetCustomerDetails();
    }

    public class CustomerType { }
    public class BasicCustomer: CustomerType { }
    public class PlatinumCustomer: CustomerType { }
}

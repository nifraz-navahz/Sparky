using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CustomerNUnitTests
    {
        private Customer customer;
        [SetUp]
        public void SetUp() 
        {
            customer = new Customer();
        }

        [Test]
        public void CombineNamesAndGreet_InputTwoStrings_GetGreeting()
        {
            customer.CombineNamesAndGreet("Nifraz", "Navahz");
            Assert.Multiple(() =>
            {
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello Nifraz Navahz!"));
                Assert.That(customer.GreetMessage, Does.StartWith("H"));
                Assert.That(customer.GreetMessage, Does.EndWith("!"));
                Assert.That(customer.GreetMessage, Does.Contain("nifraz").IgnoreCase.And.Contain("navahz").IgnoreCase);
                Assert.That(customer.GreetMessage, Does.Match("[A-Z]{1}[a-z]+!"));
            });
        }

        [Test]
        public void GreetMessage_NotGreeted_GetNull()
        {
            //customer.CombineNamesAndGreet("Nifraz", "Navahz");
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountRange()
        {
            customer.CombineNamesAndGreet("Nifraz", "Navahz");
            var actual = customer.Discount;
            Assert.That(actual, Is.InRange(10, 35));
        }

        [Test]
        public void CombineNamesAndGreet_InputFirstNameOnly_GetNotNull()
        {
            customer.CombineNamesAndGreet("Kasun", "");
            var actual = customer.GreetMessage;
            Assert.NotNull(actual);
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        [Test]
        public void CombineNamesAndGreet_InputFirstNameEmpty_ThrowsException()
        {
            Assert.Multiple(() =>
            {
                //exception message
                var exception = Assert.Throws<ArgumentException>(() => customer.CombineNamesAndGreet("", "Karindra"));
                var actual = exception.Message;
                Assert.AreEqual("Empty First Name", actual);
                Assert.That(() => customer.CombineNamesAndGreet("", "Karindra"), Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));

                //only the exception
                Assert.Throws<ArgumentException>(() => customer.CombineNamesAndGreet("", "Karindra"));
                Assert.That(() => customer.CombineNamesAndGreet("", "Karindra"), Throws.ArgumentException);
            });
        }

        [Test]
        public void CustomerType_LessThan100Orders_GetBasicType()
        {
            customer.OrderCount = 50;
            var actual = customer.GetCustomerDetails();
            Assert.That(actual, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_MoreThan100Orders_GetPlatinumType()
        {
            customer.OrderCount = 150;
            var actual = customer.GetCustomerDetails();
            Assert.That(actual, Is.TypeOf<PlatinumCustomer>());
        }
    }
}

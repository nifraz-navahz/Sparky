using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount? account;
        [SetUp]
        public void Setup()
        {
            //account = new BankAccount(new FakeLogBook());
        }

        //[Test]
        //public void DepositUsingFakeLogBook_100_ReturnTrue()
        //{
        //    var account = new BankAccount(new FakeLogBook());

        //    var actual = account.Deposit(100);
        //    Assert.That(actual, Is.True);
        //    Assert.That(account.GetBalance, Is.EqualTo(100));
        //}

        [Test]
        public void DepositUsingMoq_100_ReturnTrue()
        {
            var logBookMock = new Mock<ILogBook>();
            //logBookMock.Setup(x => x.LogMessage("Fake Log Message!"));
            var account = new BankAccount(logBookMock.Object);

            var actual = account.Deposit(100);
            Assert.That(actual, Is.True);
            Assert.That(account.GetBalance, Is.EqualTo(100));
        }

        [Test]
        public void Withdraw_Balance200Amount100_ReturnTrue()
        {
            var logBookMock = new Mock<ILogBook>(MockBehavior.Strict);
            logBookMock.Setup(x => x.LogMessage(It.IsAny<string>()));
            logBookMock.Setup(x => x.LogMessageToDb(It.IsAny<string>())).Returns(true);
            logBookMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<decimal>(y => y >= 0))).Returns(true);
            logBookMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<decimal>(y => y < 0))).Returns(false);

            var account = new BankAccount(logBookMock.Object);

            account.Deposit(200);
            var result = account.Withdraw(100);
            Assert.IsTrue(result);
        }

        [Test]
        public void Withdraw_Balance200Amount300_ReturnFalse()
        {
            //var logBookMock = new Mock<ILogBook>(MockBehavior.Strict);
            var logBookMock = new Mock<ILogBook>();
            //logBookMock.Setup(x => x.LogMessage(It.IsAny<string>()));
            //logBookMock.Setup(x => x.LogMessageToDb(It.IsAny<string>())).Returns(true);
            //logBookMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<decimal>(y => y >= 0))).Returns(true);
            //logBookMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<decimal>(y => y < 0))).Returns(false);
            logBookMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange<decimal>(0, decimal.MaxValue, Moq.Range.Inclusive))).Returns(true);

            var account = new BankAccount(logBookMock.Object);

            account.Deposit(200);
            var result = account.Withdraw(300);
            Assert.IsFalse(result);
        }

        [Test]
        public void LogMessageAndReturnString_InputString_ReturnLowercase()
        {
            var logBookMock = new Mock<ILogBook>();
            logBookMock.Setup(x => x.LogMessageAndReturnString(It.IsAny<string>())).Returns((string x) => x?.ToLower());

            var expected = "hello";
            var actual = logBookMock.Object.LogMessageAndReturnString("HeLlO");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LogWithOutputResult_InputStringAndOutput_ReturnTrue()
        {
            var logBookMock = new Mock<ILogBook>();
            var output = "hello";
            logBookMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(), out output)).Returns(true);

            var result = string.Empty;
            var actual = logBookMock.Object.LogWithOutputResult("Nifraz", out result);

            Assert.IsTrue(actual);
            Assert.AreEqual(output, result);

        }

        [Test]
        public void LogWithRefObj_InputCustomerRef_ReturnTrue()
        {
            var logBookMock = new Mock<ILogBook>();
            var customer1 = new Customer();
            var customer2 = new Customer();
            logBookMock.Setup(x => x.LogWithRefObj(ref customer1)).Returns(true);

            var result1 = logBookMock.Object.LogWithRefObj(ref customer1);
            var result2 = logBookMock.Object.LogWithRefObj(ref customer2);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

        }

        [Test]
        public void LogBook_SetAndGetSeverityAndType()
        {
            var logBookMock = new Mock<ILogBook>();

            logBookMock.Setup(x => x.Severity).Returns(10);
            logBookMock.Setup(x => x.Type).Returns("warning");

            logBookMock.SetupAllProperties();

            logBookMock.Object.Severity = 100;
            logBookMock.Object.Type = "error";

            Assert.That(logBookMock.Object.Severity, Is.EqualTo(100));
            Assert.That(logBookMock.Object.Type, Is.EqualTo("error"));

            //callbacks
            var tempStr = "Hi, ";
            logBookMock.Setup(x => x.LogMessageToDb(It.IsAny<string>()))
                .Returns(true)
                .Callback((string str) => tempStr += str);
            logBookMock.Object.LogMessageToDb("Nifraz");
            Assert.That(tempStr, Is.EqualTo("Hi, Nifraz"));

            var tempInt = 5;
            logBookMock.Setup(x => x.LogMessageToDb(It.IsAny<string>()))
                .Callback(() => tempInt++)
                .Returns(true)
                .Callback((string str) => tempInt--);
            logBookMock.Object.LogMessageToDb("Nifraz");
            Assert.That(tempInt, Is.EqualTo(5));
        }

        [Test]
        public void LogBook_VerifyPropertyAccessedAndMethodInvoked()
        {
            var logBookMock = new Mock<ILogBook>();
            var account = new BankAccount(logBookMock.Object);
            account.Deposit(500);
            Assert.That(account.Balance, Is.EqualTo(500));

            //verification
            logBookMock.Verify(x => x.LogMessage(It.IsAny<string>()), Times.Exactly(2));
            logBookMock.Verify(x => x.LogMessage("Test"), Times.Exactly(1));
            logBookMock.VerifySet(x => x.Severity = 101, Times.AtLeastOnce);
            logBookMock.VerifyGet(x => x.Severity, Times.Once);
        }
    }
}

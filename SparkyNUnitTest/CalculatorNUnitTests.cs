using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        private Calculator calc;
        [SetUp]
        public void SetUp()
        {
            calc = new Calculator();
        }

        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            //Calculator calc = new();

            //Act
            var result = calc.AddNumbers(10, 20);

            //Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        public void IsOdd_InputOddInt_GetTrue()
        {
            //Arrange
            //var calc = new Calculator();
            //Act
            var result = calc.IsOdd(5);
            //Assert
            Assert.AreEqual(true, result);
            Assert.That(result, Is.EqualTo(true));
            Assert.True(result);
        }

        [Test]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(882)]
        public void IsOdd_InputEvenInt_GetFalse(int number)
        {
            //Arrange
            //var calc = new Calculator();
            //Act
            var result = calc.IsOdd(number);
            //Assert
            Assert.AreEqual(false, result);
            Assert.That(result, Is.EqualTo(false));
            Assert.False(result);
        }

        [Test]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(4, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = false)]
        public bool IsOdd_InputNumber_ReturnTrueIfOdd(int number)
        {
            var calc = new Calculator();
            return calc.IsOdd(number);
        }

        [Test]
        [TestCase(3, 4, ExpectedResult = 7)]
        [TestCase(3, 5, ExpectedResult = 8)]
        [TestCase(6, 5, ExpectedResult = 11)]
        public int AddNumbers_InputTwoNumbers_GetAddition(int number1, int number2)
        {
            var calc = new Calculator();
            return calc.AddNumbers(number1, number2);
        }

        [Test]
        [TestCase(5.4, 10.5)] //15.9
        [TestCase(5.43, 10.53)] //15.93
        [TestCase(5.49, 10.59)] //16.08
        public void AddDoubleNumbers_InputTwoDouble_GetAddition(double number1, double number2)
        {
            var calc = new Calculator();
            var result = calc.AddDoubleNumbers(number1, number2);
            Assert.AreEqual(15.9, result, 0.2);
        }

        [Test]
        public void GetOddNumbers_InputRange_GetNumbers()
        {
            var expected = new List<int> { 5, 7, 9 };
            var actual = calc.GetOddNumbers(4, 10).ToList();
            Assert.That(actual, Is.EquivalentTo(expected));
            Assert.AreEqual(expected,actual);
            Assert.Contains(7, actual);
            Assert.That(actual, Does.Contain(7));
            Assert.That(actual, Is.Not.Empty);
            Assert.That(actual.Count, Is.EqualTo(3));
            Assert.That(actual, Has.No.Member(50));
            Assert.That(actual, Is.Ordered.Ascending);
            Assert.That(actual, Is.Unique);
        }
    }
}

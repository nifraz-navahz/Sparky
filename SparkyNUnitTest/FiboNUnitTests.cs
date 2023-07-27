using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class FiboNUnitTests
    {
        private Fibo fibo;
        [SetUp]
        public void Setup()
        {
            fibo = new Fibo();
        }

        [Test]
        public void GetFiboSeries_InputRange1_ReturnSeries()
        {
            var expected = new List<int> { 0 };

            fibo.Range = 1;
            var series = fibo.GetFiboSeries();

            Assert.That(series, Is.Not.Empty);
            Assert.That(series, Is.Ordered);
            Assert.That(series, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetFiboSeries_InputRange6_ReturnSeries()
        {
            var expected = new List<int> { 0, 1, 1, 2, 3, 5 };

            fibo.Range = 6;
            var series = fibo.GetFiboSeries();

            Assert.That(series, Does.Contain(3));
            //Assert.That(series, Has.Member(3));
            Assert.That(series.Count, Is.EqualTo(6));
            Assert.That(series, Does.Not.Contain(4));
            //Assert.That(series, Has.No.Member(4));
            Assert.That(series, Is.EquivalentTo(expected));
        }
    }
}

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
    public class ProductNUnitTests
    {
        [Test]
        public void GetDiscountedPrice_PatinumCustomer_Get20Discount()
        {
            var product = new Product { Price = 50 };
            var result = product.GetDiscountedPrice(new Customer { IsPlatinum = true } );

            Assert.That(result, Is.EqualTo(50 * .8));
        }

        [Test]
        public void GetDiscountedPriceMoqAbuse_PatinumCustomer_Get20Discount()
        {
            var customerMock = new Mock<ICustomer>();
            customerMock.Setup(x => x.IsPlatinum).Returns(true);

            var product = new Product { Price = 50 };
            var result = product.GetDiscountedPrice(customerMock.Object);

            Assert.That(result, Is.EqualTo(50 * .8));
        }
    }
}

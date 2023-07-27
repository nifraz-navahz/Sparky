using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public decimal GetDiscountedPrice(Customer customer)
        {
            if (customer.IsPlatinum)
            {
                return Price * .8m;
            }
            return Price;
        }

        public decimal GetDiscountedPrice(ICustomer customer)
        {
            if (customer.IsPlatinum)
            {
                return Price * .8m;
            }
            return Price;
        }

    }
}

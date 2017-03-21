using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Reports
{
    public class ObjectsSource
    {

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                products.Add(new Product()
                {
                    Code = i,
                    Price = i * 1.5,
                });
            }
            return products;
        }
    }

    public class Product
    {
        public int Code { get; set; }
        public double Price { get; set; }
    }
}

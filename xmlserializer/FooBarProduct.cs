using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace xmlserializer
{
    public class FooBarProduct : Product
    {

        public FooBarProduct(String bez,decimal price) {
            this.artikelbezeichnung = bez;
            this.preis = price;
            this.ProductType = typeof(FooBarProduct);
        }

        public FooBarProduct() {

        }
    }
}

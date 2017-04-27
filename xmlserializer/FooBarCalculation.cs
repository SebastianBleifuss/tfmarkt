using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer
{
    class FooBarCalculation : models.Calculation
    {
        public FooBarCalculation() : base(){
            CalculationType = typeof(FooBarCalculation);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer
{
    public class FooBarCalculation : Models.Calculation
    {
        public FooBarCalculation() : base(){
            CalculationType = typeof(FooBarCalculation);
            
        }
    }
}

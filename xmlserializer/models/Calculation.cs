using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace xmlserializer.Models
{
    public abstract class Calculation
    {
        public Guid Identifier = Guid.NewGuid();
        public String Description = "undefined";
        public Type CalculationType;


        public override string ToString()
        {
            return Description;

        }

        public static Type GetType(String AssemblyQualifiedName)
        {
            if (AssemblyQualifiedName.Equals(typeof(FooBarCalculation).AssemblyQualifiedName))
            {
                return typeof(FooBarCalculation);
            }
            else
            {
                throw new NotSupportedException(AssemblyQualifiedName + " is not supported!");
            }
            //Erweitern!
        }


    }
}

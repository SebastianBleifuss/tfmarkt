using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace xmlserializer.Models
{
    /// <summary>
    /// Abstract class defines all calculations
    /// </summary>
    public abstract class Calculation
    {
        /// <summary>
        /// Identifier of calculations
        /// </summary>
        public Guid Identifier { get; internal protected set; }

        /// <summary>
        /// Description used as displayed name
        /// </summary>
        public String Description { get; internal protected set; }

        /// <summary>
        /// Type of calculation
        /// </summary>
        public Type CalculationType { get; internal protected set; }

        /// <summary>
        /// Amount of the selected product
        /// </summary>
        public int Amount { get; internal protected set; }

        /// <summary>
        /// Selected product
        /// </summary>
        public Product SelectedProduct { get; internal protected set; }

        public Calculation(){
        Description = "undefined";
        }

        /// <summary>
        /// set CalculationType
        /// </summary>
        /// <param name="t">Type of calculation</param>
        public void setCalculationType(Type t)
        {
            if (typeof(Calculation).IsAssignableFrom(t))//Check if passed type inherit base type
            {
                this.CalculationType = t;
            }
            else
            {
                throw new InvalidOperationException("Not inherit Calculation");
            }
        }

        /// <summary>
        /// Return Description defines the calculation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description;

        }

        /// <summary>
        /// Return CalculationType of calculation if the AssemblyQualifiedName matches
        /// </summary>
        /// <param name="AssemblyQualifiedName">AssemblyQualifiedName</param>
        /// <returns>Type of calculation</returns>
        public static Type GetType(String AssemblyQualifiedName)
        {
            if (AssemblyQualifiedName.Equals(typeof(FooBarCalculation).AssemblyQualifiedName))//Check if passed type inherit base type
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

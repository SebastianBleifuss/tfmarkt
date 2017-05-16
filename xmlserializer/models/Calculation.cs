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
        public Guid Identifier { get; set; }

        /// <summary>
        /// Description used as displayed name
        /// </summary>
        public String Description { get;  set; }

        /// <summary>
        /// Type of calculation
        /// </summary>
        public Type CalculationType { get; set; }

        /// <summary>
        /// Length of the room
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// Width of the room
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// With Fliesenkleber for the Fliesenkalkuation
        /// </summary>
        public bool WithExtraProduct { get;  set; }


        public Product SelectedProduct { get; set; }

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

    }
}

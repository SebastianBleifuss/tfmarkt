using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace xmlserializer.models
{
    public abstract class Calculation
    {
        public readonly Guid IDENTIFIER = Guid.NewGuid();
        public DateTime date = DateTime.Now;
        public Type CalculationType;
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Common.Exceptions
{
    public class IngredientFoodRelationNotFoundException : Exception
    {
        public IngredientFoodRelationNotFoundException() : base() { }

        public IngredientFoodRelationNotFoundException(string message) : base(message) { }

        public IngredientFoodRelationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}

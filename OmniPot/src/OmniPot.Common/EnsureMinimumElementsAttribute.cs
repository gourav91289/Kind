using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Common
{
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int minElements; 
        public EnsureMinimumElementsAttribute(int minElements)
        {
            this.minElements = minElements; 
        }

        public override bool IsValid(object value)
        {
            var list = value as IList; 
            if (null != list)
            {
                return list.Count > minElements; 
            }
            return false; 
        }
    }
}

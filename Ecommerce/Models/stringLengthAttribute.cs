using System;

namespace Ecommerce.Models
{
    internal class stringLengthAttribute : Attribute
    {
        private int v;
        private int minLengthAttribute;

        public stringLengthAttribute(int v, string ErrorMessage, int MinLengthAttribute)
        {
            this.v = v;
            this.ErrorMessage = ErrorMessage;
            minLengthAttribute = MinLengthAttribute;
        }

        public string ErrorMessage { get; set; }
    }
}
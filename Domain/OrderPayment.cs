using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderPayment
    {
        public string UniqueCode { get; set; }
        public string Description { get; set; }
        public IReadOnlyDictionary<string, string> Parameters { get; }
        public OrderPayment(string uniqueCode, string description,  IReadOnlyDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentNullException(nameof(uniqueCode));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            UniqueCode = uniqueCode;
            Description = description;
            Parameters = parameters;
        }
    }
}

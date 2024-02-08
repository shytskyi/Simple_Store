using Domain.Contracts.Fields;

namespace Domain.Contracts
{
    public class Form
    {
        public string UniqueCode { get; set; }
        public int OrderId { get; set; }
        public int Step { get; set; }
        public bool IsFinal { get; set; }
        public IReadOnlyList<Field> Fields { get; }
        public Form(string uniqueCode, int orderId, int step, bool isFinal, IEnumerable<Field> fields)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentNullException(nameof(uniqueCode));
            if (step < 1)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            UniqueCode = uniqueCode;
            OrderId = orderId;
            Step = step;
            IsFinal = isFinal;
            Fields = fields.ToArray();
        }
    }
}

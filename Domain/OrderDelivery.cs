namespace Domain
{
    public class OrderDelivery
    {
        public string UniqueCode { get; set; }
        public string Description { get; set; }
        public decimal AmountDelivery { get; set; }
        public IReadOnlyDictionary<string, string> Parameters  { get; }
        public OrderDelivery(string uniqueCode, string description, decimal amountDelivery,  IReadOnlyDictionary<string, string> parameters)
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
            AmountDelivery = amountDelivery;
        }
    }
}

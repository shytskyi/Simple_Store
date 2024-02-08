namespace Domain.Contracts.Fields
{
    public class SelectionField : Field
    {
        public IReadOnlyDictionary<string, string> Items { get; set; }
        public SelectionField(string label, string name, string value, IReadOnlyDictionary<string, string> items)
                                                        : base(label, name, value)
        {
            Items = items;
        }
    }
}

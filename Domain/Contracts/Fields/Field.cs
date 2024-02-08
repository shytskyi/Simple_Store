namespace Domain.Contracts.Fields
{
    public abstract class Field
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        protected Field(string label, string name, string value)
        {
            Label = label;
            Name = name;
            Value = value;
        }
    }
}

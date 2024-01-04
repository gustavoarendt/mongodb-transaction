namespace Producer.Transactions
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public bool IsExpense { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public decimal Value { get; private set; }

        public Transaction(string description, bool isExpense, decimal value)
        {
            Id = Guid.NewGuid();
            Description = description;
            IsExpense = isExpense;
            UpdatedAt = DateTime.UtcNow;
            Value = value;
        }

        public void Validate()
        {
            if (Value <= 0)
            {
                throw new InvalidOperationException("Value cannot be less or equals than zero.");
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new InvalidOperationException("Description can't not be null or empty");
            }
        }
    }
}

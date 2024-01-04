namespace Consumer.Transactions
{
    public record TransactionModel(Guid Id, string Description, bool IsExpense, DateTime UpdatedAt, decimal Value);
}

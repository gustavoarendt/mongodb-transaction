namespace Producer.Transactions
{
    public record TransactionInputModel(string Description, bool IsExpense, decimal Price)
    {
        public static Transaction CreateTransaction(string Description, bool IsExpense, decimal Price)
        {
            return new Transaction(Description, IsExpense, Price);
        }
    }
}

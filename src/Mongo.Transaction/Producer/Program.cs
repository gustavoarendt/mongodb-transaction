using Producer.Transactions;

var builder = WebApplication.CreateBuilder(args);

_ = builder.Services.AddEndpointsApiExplorer();
_ = builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

_ = app.UseHttpsRedirection();

_ = app.MapPost("/create", async (TransactionInputModel model) =>
{
    var transaction = TransactionInputModel.CreateTransaction(model.Description, model.IsExpense, model.Price);
    await new TransactionProducer().PublishTransactionAsync(transaction, "transaction");
})
.WithName("CreateTransaction");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
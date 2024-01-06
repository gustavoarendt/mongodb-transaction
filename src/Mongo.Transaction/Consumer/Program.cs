using Confluent.Kafka;
using Consumer.Transactions;
using MongoDB.Driver;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var config = new ConsumerConfig
{
    BootstrapServers = "172.17.208.1:9094",
    GroupId = "transaction-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

var connectionString = builder.Configuration["TransactionDatabase:ConnectionString"];
var databaseName = builder.Configuration["TransactionDatabase:DatabaseName"];
var collectionName = builder.Configuration["TransactionDatabase:CollectionName"];

var mongoClient = new MongoClient(connectionString);
var database = mongoClient.GetDatabase(databaseName);
var collection = database.GetCollection<TransactionModel>(collectionName);

while (true)
{
    consumer.Subscribe("transaction");
    try
    {
        var consumeResult = consumer.Consume();
        Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");

        var processedMessage = JsonSerializer.Deserialize<TransactionModel>(consumeResult.Message.Value);

        collection.InsertOne(processedMessage!);
    }
    catch (ConsumeException ex)
    {
        Console.WriteLine($"Error consuming message: {ex.Message}");
    }
    catch (MongoException ex)
    {
        Console.WriteLine($"Error interacting with MongoDB: {ex.Message}");
    }
}

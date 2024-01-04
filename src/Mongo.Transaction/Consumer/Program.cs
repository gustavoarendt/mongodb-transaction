using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "172.17.208.1:9094",
    GroupId = "transaction-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

while (true)
{
    consumer.Subscribe("transaction");
    try
    {
        var consumeResult = consumer.Consume();
        Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
    }
    catch (ConsumeException ex)
    {
        Console.WriteLine($"Error consuming message: {ex.Message}");
    }
}
using Confluent.Kafka;
using System;
using System.Text.Json;

namespace Producer.Transactions
{
    public class TransactionProducer
    {
        private readonly ProducerConfig _config;

        public TransactionProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "host.docker.internal:9092"
            };
        }

        public async Task PublishTransactionAsync(Transaction transaction, string topic)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                try
                {
                    string transactionJson = JsonSerializer.Serialize(transaction);
                    await producer.ProduceAsync(topic, new Message<Null, string> { Value = transactionJson });
                    producer.Flush();
                    Console.WriteLine($"Transaction produced to topic {topic}: {transactionJson}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error producing transaction: {e.Message}");
                }
            }
        }
    }
}

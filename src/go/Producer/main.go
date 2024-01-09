package main

import (
	"fmt"

	"github.com/confluentinc/confluent-kafka-go/kafka"
)

func main() {
	config := &kafka.ConfigMap{
		"bootstrap.servers": []interface{}{"127.0.0.1:9092"},
		"group.id":          "my-producer-group",
	}

	producer, err := kafka.NewProducer(config)
	if err != nil {
		fmt.Println("Failed to create producer:", err)
		return
	}

	message := &kafka.Message{
		TopicPartition: kafka.TopicPartitions{
			kafka.TopicPartition.Topic: "transaction"
		}
		Value:          []byte("Hello, Kafka!"),
	}

	err = producer.Produce(message, nil)
	if err != nil {
		fmt.Println("Failed to produce message:", err)
		return
	}

	fmt.Println("Successfully produced message!")

	producer.Close()
}

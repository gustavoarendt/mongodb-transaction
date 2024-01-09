package main

import (
	"log"

	ckafka "github.com/confluentinc/confluent-kafka-go/kafka"
)

func main() {
	producer := NewKafkaProducer()
	Publish("Some transaction converted", "transaction", producer, nil)
	producer.Flush(1000)
}

func NewKafkaProducer() *ckafka.Producer {
	configMap := &ckafka.ConfigMap{
		"bootstrap.servers": "172.17.208.1:9094",
	}

	p, err := ckafka.NewProducer(configMap)
	if err != nil {
		log.Println(err.Error())
	}
	return p
}

func Publish(msg, topic string, producer *ckafka.Producer, key []byte) error {
	message := &ckafka.Message{
		Value:          []byte(msg),
		TopicPartition: ckafka.TopicPartition{Topic: &topic, Partition: ckafka.PartitionAny},
		Key:            key,
	}
	err := producer.Produce(message, nil)
	if err != nil {
		return err
	}
	return nil
}

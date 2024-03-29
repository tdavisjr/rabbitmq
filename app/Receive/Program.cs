﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
  class Program
  {
    static void Main(string[] args)
    {
      var factory = new ConnectionFactory { HostName = "localhost" };
      using var connection = factory.CreateConnection();
      using var channel = connection.CreateModel();

      Console.Write("Queue Name: ");
      var queue = Console.ReadLine();

      channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

      var consumer = new EventingBasicConsumer(channel);

      consumer.Received += (model, ea) =>
      {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine($"Received {message}");

      };

      channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

      Console.WriteLine("Press [enter] to exit. ");
      Console.ReadLine();

    }
  }
}

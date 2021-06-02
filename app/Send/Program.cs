using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
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

      Console.Write("Message To Send: ");
      var message = Console.ReadLine();
      var body = Encoding.UTF8.GetBytes(message);

      channel.BasicPublish(exchange: "",
                           routingKey: "hello",
                           body: body);

      Console.WriteLine($"Sent {message} to queue: {queue}");

      Console.WriteLine(" Press [enter] to exit.");
      Console.ReadLine();

    }
  }
}

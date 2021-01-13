using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQSendConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Missing Arguments expecting dotnet run <hostName> <queueName> <userName> <password> <message>");
                return;
            }

            var hostName = args[0];
            var queueName = args[1];            
            var userName = args[2];
            var password = args[3];
            var message = args[4];

            //Create the queue if it doesn't exist
            var factory = new ConnectionFactory() 
            { 
                HostName = hostName, 
                UserName=userName, 
                Password=password 
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string msg = message;
                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", msg);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


        }
    }
}

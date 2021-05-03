using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WiproTeste.Models;

namespace Wipro.Servico
{
    public class Emissor
    {
        public string AddItemFila(List<Moeda> itens)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cotaqueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var message = System.Text.Json.JsonSerializer.Serialize(itens);

                return message;

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                        routingKey: "cotaqueue",
                                        basicProperties: null,
                                        body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

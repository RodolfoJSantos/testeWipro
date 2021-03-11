using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using WiproTeste.Models;

namespace Wipro.Servico
{
    public class Worker
    {
        Processamento arquivos = new Processamento();
        public void GetItemFila()
        {
            var result = new Moeda();
            var lista = new List<Moeda>();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cotaqueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        lista = JsonSerializer.Deserialize<List<Moeda>>(message);

                        arquivos.CriarArquivo(lista);
                        //CriarArquivo(lista);
                        //ObterCotacao();

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao consumir fila");
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };

                var str = lista.ToArray();
                channel.BasicConsume(queue: "cotaqueue",
                                     autoAck: false,
                                     consumer: consumer);


                Console.ReadLine();
            }
        }
    }
}

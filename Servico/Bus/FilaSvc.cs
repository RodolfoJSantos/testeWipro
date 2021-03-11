using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Servico.Bus
{
    public class Comunicacao
    {
        private const string hostName = "localhost";
        private const string fila = "filaExemplo";

        public void Enviar(string mensagem)
        {
            var factory = new ConnectionFactory() { HostName = hostName };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: fila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: fila,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", mensagem);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        public void Consumir()
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                model.QueueDeclare(queue: fila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(model);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                string consumerTag = model.BasicConsume(queue: fila,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static IConnection BuildConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "exemplo_amqp",
                HostName = "localhost",
                Port = 5672,
                AutomaticRecoveryEnabled = true
            };

            IConnection conn = null;
            int retryCount = 0;
            do
            {

                try
                {
                    conn = factory.CreateConnection();
                }
                catch (BrokerUnreachableException rootException) when (DecomposeExceptionTree(rootException).Any(it => it is ConnectFailureException && (it?.InnerException?.Message?.Contains("Connection refused") ?? false)))
                {
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds((retryCount + 1) * 2));
                }
                catch
                {
                    throw;
                }

            } while (conn == null && ++retryCount <= 5);
            if (conn == null)
                throw new InvalidOperationException($"Não foi possível conectar ao RabbitMQ após {retryCount} tentativas");
            return conn;
        }

        public static IEnumerable<Exception> DecomposeExceptionTree(Exception currentException)
        {
            while (currentException != null)
            {
                yield return currentException;
                currentException = currentException.InnerException;
            }
        }
    }
}

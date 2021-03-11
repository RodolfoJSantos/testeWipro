using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiBackGroundServices.Services
{
    public class InfoWorker : IHostedService
    {
        private Timer _timer;
        private string mensagem = "mensagem inicial";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Proccess started { nameof(InfoWorker)}");

            _timer = new Timer(DoWork, null , TimeSpan.Zero, TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Process finished {nameof(InfoWorker)}");

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Console.WriteLine("");
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrintServer
{
    public class Producer
    {
        public string Name { get; }

        private readonly IQueue _queue;

        private static readonly Random Random = new Random();

        public Producer(string name, IQueue queue)
        {
            Name = name;
            _queue = queue;
        }

        public async Task StartProducingAsync(int numberOfFiles, CancellationToken cancellationToken)
        {
            var i = 0;

            while (!cancellationToken.IsCancellationRequested && i < numberOfFiles)
            {
                var fileName = $"{Guid.NewGuid()}.txt";
                var number = Random.Next(1, 50);

                var printJob = new PrintJob(fileName, number);

                Console.WriteLine($"#{Name}# produzindo arquivo '{printJob.Name}', número de páginas {printJob.NumberOfPages}.");

                try
                {
                    _queue.AddBack(printJob);
                }
                catch (FullQueueException)
                {
                    // TODO: Tentar novamente, gravar logs, etc...
                    Console.WriteLine($"#{Name}# FullQueueException");
                }

                var delay = Random.Next(1, 5);

                await Task.Delay(TimeSpan.FromSeconds(delay));

                i++;
            }

            Console.WriteLine($"#{Name}# Finalizado");
        }
    }
}

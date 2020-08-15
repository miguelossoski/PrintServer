using System;
using System.Threading;

namespace PrintServer
{
    class Program
    {
        private const int NumberOfProducers = 2;
        private const int NumberOfFiles = 10;
        private const int QueueCapacity = 25;

        static void Main()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var queue = new CircularQueue(QueueCapacity);

            var printer = new Printer("XP Inc Printer", queue, cancellationTokenSource);

            _ = printer.StartPrintingAsync();

            StartProducers(queue, cancellationTokenSource.Token);

            Console.WriteLine("Pressione Enter para desligar a impressora");
            Console.ReadKey();

            printer.Halt();

            Console.ReadKey();
        }

        private static void StartProducers(IQueue queue, CancellationToken cancellationToken)
        {
            for (var i = 1; i <= NumberOfProducers; i++)
            {
                var producerName = $"Producer{i}";

                var producer = new Producer(producerName, queue);

                _ = producer.StartProducingAsync(NumberOfFiles, cancellationToken);
            }
        }
    }
}
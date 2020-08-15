using System;
using System.Threading;

namespace PrintServer
{
    class Program
    {
        static void Main()
        {
            const int numberOfProducers = 2;
            const int numberOfFiles = 5;

            const int queueCapacity = 10;
                
            var cancellationToken = new CancellationTokenSource();

            var queue = new CircularQueue(queueCapacity);

            var printer = new Printer("XP Inc Printer", queue, cancellationToken);

            _ = printer.StartPrintingAsync();

            for (var i = 1; i <= numberOfProducers; i++)
            {
                var producerName = $"Producer{i}";

                var producer = new Producer(producerName, queue, cancellationToken);

                _ = producer.StartProducingAsync(numberOfFiles);
            }

            // Pressione Enter para desligar a impressora
            Console.ReadKey();

            printer.Halt();

            Console.ReadKey();
        }
    }
}

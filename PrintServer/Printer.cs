using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrintServer
{
    public class Printer
    {
        public string Name { get; }

        private readonly IQueue _queue;
        private readonly CancellationTokenSource _cancellationToken;

        private const int MillisecondPerPage = 50;

        public Printer(string name, IQueue queue, CancellationTokenSource cancellationToken)
        {
            Name = name;
            _queue = queue;
            _cancellationToken = cancellationToken;

            Console.WriteLine("[Printer] Ligando...");
        }

        public async Task StartPrintingAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                await WaitQueue();

                if (_cancellationToken.IsCancellationRequested)
                    break;

                await Print();
            }

            Console.WriteLine("[Printer] Impressora desligada...");
        }

        private async Task WaitQueue()
        {
            var showMessage = true;

            while (_queue.IsEmpty() && !_cancellationToken.IsCancellationRequested)
            {
                if (showMessage)
                    Console.WriteLine("[Printer] Esperando por trabalho de impressão...");

                await Task.Delay(TimeSpan.FromSeconds(1));

                showMessage = false;
            }

            await Task.CompletedTask;
        }

        private async Task Print()
        {
            var printJob = _queue.RemoveFront();

            Console.WriteLine($"[Printer] Imprimindo '{printJob.Name}'...");

            var totalTime = printJob.NumberOfPages * MillisecondPerPage;

            await Task.Delay(TimeSpan.FromMilliseconds(totalTime));

            Console.WriteLine($"[Printer] '{printJob.Name}' ok.");
        }

        public void Halt()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }
    }
}
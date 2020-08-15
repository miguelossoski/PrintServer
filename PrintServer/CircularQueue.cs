using System.Collections.Concurrent;
using System.Linq;

namespace PrintServer
{
    public class CircularQueue : IQueue
    {
        private readonly ConcurrentQueue<PrintJob> _jobs;

        public int Capacity { get; }

        public CircularQueue(int capacity)
        {
            Capacity = capacity;

            _jobs = new ConcurrentQueue<PrintJob>();
        }

        public void AddBack(PrintJob job)
        {
            if (GetNumberOfJobs() == Capacity)
                throw new FullQueueException();

            _jobs.Enqueue(job);
        }

        public int GetNumberOfJobs()
        {
            return _jobs.Count;
        }

        public bool IsEmpty()
        {
            return !_jobs.Any();
        }

        public PrintJob RemoveFront()
        {
            _jobs.TryDequeue(out var job);

            return job;
        }
    }
}
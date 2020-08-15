namespace PrintServer
{
    public interface IQueue
    {
        void AddBack(PrintJob job);
        PrintJob RemoveFront();
        bool IsEmpty();
        int GetNumberOfJobs();
    }
}

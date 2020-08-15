namespace PrintServer
{
    public class PrintJob
    {
        public string Name { get; }
        public int NumberOfPages { get; }

        public PrintJob(string name, int numberOfPages)
        {
            Name = name;
            NumberOfPages = numberOfPages;
        }
    }
}

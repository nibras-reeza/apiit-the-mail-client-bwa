namespace TheMailClient.Domain.Model
{
    public class File
    {
        public SyncAccount Account { get; internal set; }

        public string Name { get; internal set; }

        public string Type { get; internal set; }

        public string URL { get; internal set; }

        public string Id { get; internal set; }

        public long Size { get; internal set; }
    }
}
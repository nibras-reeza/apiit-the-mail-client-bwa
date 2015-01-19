namespace TheMailClient.Domain.Model
{
    public class PersonalizationConfig
    {
        public PersonalizationConfig()
        {
            Tag = new Rectangle();
            Thread = new Rectangle();
            Header = new Rectangle();
        }

        public Rectangle Tag { get; private set; }

        public Rectangle Thread { get; private set; }

        public Rectangle Header { get; private set; }

        public string UICulture { get; set; }

        public string Theme { get; set; }
    }

    public class Rectangle
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
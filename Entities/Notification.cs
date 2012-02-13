namespace Brahmastra.FoursquareApi.Entities
{
    public class Notification<T>
    {
        public T Item { get; private set; }
        public string Type { get; private set; }

        public Notification(T item, string type)
        {
            Item = item;
            Type = type;
        }
    }
}

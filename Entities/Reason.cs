namespace Brahmastra.FoursquareApi.Entities
{
    public class Reason
    {
        public string Message { get; set; }
        public string Type { get; set; }

        public Reason()
        {
            Type = "";
            Message = "";
        }
    }
}

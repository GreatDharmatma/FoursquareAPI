namespace Brahmastra.FoursquareApi.Entities
{
    public class Setting
    {
        public string Value { get; private set; }

        public string Settings { get; private set; }

        public Setting(string setting, string value)
        {
            Settings = setting;
            Value = value;
        }
    }
}

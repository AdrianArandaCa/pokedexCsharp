namespace PokedexWebService.Model
{
    public class Types
    {
        public Types(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        public string name { get; set; }
        public string url { get; set; }
    }
}
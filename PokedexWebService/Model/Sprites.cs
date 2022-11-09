namespace PokedexWebService.Model
{
    public class Sprites
    {
        public string front_default { get; set; }

        public Sprites()
        {
        }

        public Sprites(string front_default)
        {
            this.front_default = front_default;
        }
    }
}
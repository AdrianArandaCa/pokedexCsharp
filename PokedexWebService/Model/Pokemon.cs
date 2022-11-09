using System.Collections.Generic;

namespace PokedexWebService.Model
{
    public class Pokemon
    {
        public Pokemon()
        {
        }

        public string name { get; set; }
        public int id { get; set; }

        public List<Move> moves { get; set; }

        public int height { get; set; }
        public int weight { get; set; }
        public Sprites sprites { get; set; }
        public Pokemon(string nam, int id)
        {
            this.name = nam;
            this.id = id;
        }
    }
}
using System.Collections.Generic;

namespace PokedexWebService.Model
{
    public class PokeType
    {
        public PokeType()
        {
        }
        public string name { get; set; }
        public List<PokeList> pokemon { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokedexWebService.Model
{
    public class Repository
    {
        string ws1 = "https://pokeapi.co/api/v2/";

        public List<TypePokemonList> GetAllPokemon()
        {
            List<TypePokemonList> pokemons = null;
            pokemons = (List<TypePokemonList>)MakeRequest(string.Concat(ws1, "/pokemon?limit=100000&offset=0"), null, "GET", "application/json", typeof(List<TypePokemonList>));
            return pokemons;
        }

        public ResultsTypes GetType()
        {
            ResultsTypes poke = null;
            poke = (ResultsTypes)MakeRequest(string.Concat(ws1, "type/"), null, "GET", "application/json", typeof(ResultsTypes));
            return poke;
        }

        public List<Types> GetTypeOrder(ResultsTypes results)
        {
            //Types tip = new Types(" "," ");
            List<Types> poke = new List<Types>();
            List<Types> pkm = results.results.OrderBy(a => a.name).ToList();
            /*poke.Add(tip)*/
            ;
            poke.AddRange(pkm);
            return poke;
        }


        public PokeType GetTypePokemons(string tipusPokemon)
        {
            PokeType poke = null;
            poke = (PokeType)MakeRequest(string.Concat(ws1, "type/", tipusPokemon), null, "GET", "application/json", typeof(PokeType));
            return poke;
        }

        public List<PokeList> GetFiltrePokemon(List<PokeList> pkm, string nom)
        {
            List<PokeList> pokemon = null;
            pokemon = pkm.Where(a => a.pokemon.name.Contains(nom)).OrderBy(a => a.pokemon.name).ToList();
            return pokemon;
        }

        public Pokemon GetPokemons(TypePokemonList pkmn)
        {
            Pokemon poke = null;
            poke = (Pokemon)MakeRequest(string.Concat(pkmn.url), null, "GET", "application/json", typeof(Pokemon));
            return poke;
        }


        public static object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                string sb = JsonConvert.SerializeObject(JSONRequest);
                request.Method = JSONmethod; 

                if (JSONmethod != "GET")
                {
                    request.ContentType = JSONContentType;  
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                    return objResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}

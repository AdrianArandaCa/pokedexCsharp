using PokedexWebService.Model;
using PokedexWebService.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokedexWebService.Controller
{
    class Controller1
    {
        Form1 f;
        Repository repo;

        public Controller1()
        {
            f = new Form1();
            repo = new Repository();
            InitListeners();
            LoadData();
            Application.Run(f);
        }

        void LoadData()
        {

            f.cbTipus.DataSource = repo.GetTypeOrder(repo.GetType());
            f.cbTipus.DisplayMember = "name";

        }

        void InitListeners()
        {
            f.dgvPokemon.SelectionChanged += DgvPokemon_SelectionChanged;
            f.cbTipus.SelectedIndexChanged += CbTipus_SelectedIndexChanged;
            f.btmFiltrar.Click += BtmFiltrar_Click;
        }

        private void BtmFiltrar_Click(object sender, EventArgs e)
        {
            string nom = f.tbNom.Text.ToString();
            if (nom.Equals(""))
            {
                LoadData();
            }
            else
            {
                Types tipusP = f.cbTipus.SelectedItem as Types;
                PokeType poke = repo.GetTypePokemons(tipusP.name);
                List<PokeList> pokemonsFilter = repo.GetFiltrePokemon(poke.pokemon, nom);

                List<TypePokemonList> pkmn = new List<TypePokemonList>();
                Pokemon po = new Pokemon();
                List<Pokemon> listPokemon = new List<Pokemon>();
                foreach (var p in pokemonsFilter)
                {
                    pkmn.Add(p.pokemon);
                }
                foreach (var pk in pkmn)
                {
                    po = repo.GetPokemons(pk);
                    listPokemon.Add(po);
                }


                f.dgvPokemon.DataSource = listPokemon;
                f.dgvPokemon.Columns["id"].DisplayIndex = 0;
                f.dgvPokemon.Columns["height"].Visible = false;
                f.dgvPokemon.Columns["sprites"].Visible = false;
                f.dgvPokemon.Columns["weight"].Visible = false;
            }

        }

        private void CbTipus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Types tipusP = f.cbTipus.SelectedItem as Types;
            if (!tipusP.name.Equals(" "))
            {
                PokeType poke = repo.GetTypePokemons(tipusP.name);
                List<TypePokemonList> pkmn = new List<TypePokemonList>();
                Pokemon po = new Pokemon();
                List<Pokemon> listPokemon = new List<Pokemon>();
                foreach (var p in poke.pokemon)
                {
                    pkmn.Add(p.pokemon);
                }
                foreach (var pk in pkmn)
                {
                    po = repo.GetPokemons(pk);
                    listPokemon.Add(po);
                }


                f.dgvPokemon.DataSource = listPokemon;
                f.dgvPokemon.Columns["id"].DisplayIndex = 0;

                f.dgvPokemon.Columns["height"].Visible = false;
                f.dgvPokemon.Columns["sprites"].Visible = false;
                f.dgvPokemon.Columns["weight"].Visible = false;
            }
            else
            {
                LoadData();
            }







        }

        private void DgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon pokemon = f.dgvPokemon.CurrentRow.DataBoundItem as Pokemon;

            List<Moves> move = new List<Moves>();

            foreach (var m in pokemon.moves)
            {
                move.Add(m.move);
            }

            f.dgvInfo.DataSource = move;

            string sprites = pokemon.sprites.front_default;
            //Pokemon sprite = repo.GetImgPokemon(pkmn.name);
            //Sprites sprites = repo.GetImgPokemon(pokemon.name).sprites;

            if (sprites != null)
            {
                afegirImg(sprites);
                f.tbNumero.Text = pokemon.id.ToString();
                f.tbPeso.Text = pokemon.weight.ToString();
                f.tbAlto.Text = pokemon.height.ToString();
            }
            else
            {
                afegirImg("https://thumbs.dreamstime.com/b/sin-foto-ni-icono-de-" +
                    "imagen-en-blanco-cargar-im%C3%A1genes-o-falta-marca-no-disponible-" +
                    "pr%C3%B3xima-se%C3%B1al-silueta-naturaleza-simple-marco-215973362.jpg");
            }
        }

        void afegirImg(string url)
        {

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);

            MemoryStream ms = new MemoryStream(bytes);
            f.img.Image = Image.FromStream(ms);
        }
    }
}


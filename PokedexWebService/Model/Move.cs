namespace PokedexWebService.Model
{
    public class Move
    {
        public Move()
        {
        }


        public Moves move { get; set; }

        public Move(Moves move)
        {
            this.move = move;
        }
    }
}
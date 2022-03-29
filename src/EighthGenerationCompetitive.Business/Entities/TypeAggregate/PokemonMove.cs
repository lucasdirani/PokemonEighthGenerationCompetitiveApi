namespace EighthGenerationCompetitive.Business.Entities.TypeAggregate
{
    public class PokemonMove
    {
        public string MoveName { get; set; }

        public string MoveId { get; set; }

        public string MoveCategory { get; set; }

        public int? MovePower { get; set; }

        public int? MoveAccuracy { get; set; }

        public int MovePP { get; set; }

        public string MoveDetails { get; set; }
    }
}
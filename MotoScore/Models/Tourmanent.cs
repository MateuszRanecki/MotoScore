using System.ComponentModel.DataAnnotations;

namespace MotoScore.Models
{
    public class Tourmanent
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        public string Name { get; set; }

        public int LevelsCount { get; set; }

        public TournamentGroup Group { get; set; }

        public TournamentStatus Status{ get; set; } = TournamentStatus.Created;

        public IEnumerable<Contender> Contenders { get; set; } =new List<Contender>();

    }

    public enum TournamentGroup {
        A=0,
        B=1,
        C=2,
    }

    public enum TournamentStatus {
        Created = 0,
        Active = 1,
        Closed = 2,
    }
}

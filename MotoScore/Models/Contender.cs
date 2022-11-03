using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoScore.Models
{
    public class Contender
    {
        [Key]
        public Guid Id { get; set; } = new Guid();      
        public Guid TourmanentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TournamentGroup TournamentGroup { get; set; }

        public int PenaltyPointsCount { get; set; } = 0;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StartNumber { get; set; }

        public ContenderStatus Status { get; set; } = ContenderStatus.Participating;

        public int CurrentPart { get; set; } = 1;
    }


    public enum ContenderStatus {
        Participating = 0,
        Finished = 1,
        Disqualified = 2,
    }
}

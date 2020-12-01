using System;

namespace MovieContest.Domain.Entities
{
    public class Contest : BaseEntity
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public Movie Movie { get; set; }
        public long MovieId { get; set; }
        public DateTime PersistDate { get; set; }
        public bool FlagDeleted { get; set; }
    }
}

using System;

namespace MovieContest.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Actor Actor { get; set; }
        public long ActorId { get; set; }
        public Movie Movie { get; set; }
        public long MovieId { get; set; }
        public string CharacterName { get; set; }
        public string Description { get; set; }
        public bool FlagMainActor { get; set; }
        public DateTime PersistDate { get; set; }
        public bool FlagDeleted { get; set; }
    }
}

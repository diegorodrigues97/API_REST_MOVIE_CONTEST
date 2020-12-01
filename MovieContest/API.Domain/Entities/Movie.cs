using MovieContest.Domain.Model.Enumeration;
using System;
using System.Collections.Generic;

namespace MovieContest.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public EMovieGenre Genre { get; set; }
        public string Director { get; set; }
        public List<Role> Cast { get; set; } = new List<Role>();
        public long ActorId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PersistDate { get; set; }
        public bool FlagDeleted { get; set; }
    }
}

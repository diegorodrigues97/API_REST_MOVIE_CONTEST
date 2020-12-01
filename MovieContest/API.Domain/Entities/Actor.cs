using System;

namespace MovieContest.Domain.Entities
{
    public class Actor : BaseEntity
    {
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Nationality { get; set; }
        public string ProfessionalCareer { get; set; }
        public string Awards { get; set; }
        public DateTime PersistDate { get; set; }
        public bool FlagDeleted { get; set; }
    }
}

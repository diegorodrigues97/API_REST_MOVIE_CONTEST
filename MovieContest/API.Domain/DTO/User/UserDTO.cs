using MovieContest.Domain.Model.Enumeration;
using System;

namespace MovieContest.Domain.DTO.User
{
    public class UserDTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public DateTime persist_date { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public EUserType type { get; set; }
        public DateTime? last_access { get; set; }
    }
}

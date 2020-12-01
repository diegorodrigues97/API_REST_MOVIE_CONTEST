using System.Collections.Generic;


namespace MovieContest.Domain.DTO.User
{
    public class UserFilterDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int items_per_page { get; set; }
        public int page { get; set; }
        public int total_pages { get; set; }
        public int total_items { get; set; }
        public List<UserDTO> items { get; set; } = new List<UserDTO>();
    }
}

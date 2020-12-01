using MovieContest.Domain.Converter.Contract;
using MovieContest.Domain.DTO.User;
using MovieContest.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieContest.Domain.Converter
{
    public class UserConverter : IParser<UserDTO, User>, IParser<User, UserDTO>
    {
        public User Parse(UserDTO origin)
        {            
            return origin == null ? new User() : new User() {
                Id = origin.id,
                Name = origin.name,
                LastName = origin.last_name,
                Gender = origin.gender,
                DateOfBirth = origin.date_of_birth,
                PersistDate = origin.persist_date,
                Email = origin.email,
                Password = origin.password,
                Type = origin.type,
                LastAccess = origin.last_access
            };
        }

        public List<User> Parse(List<UserDTO> origin)
        {
            return origin.Select(i => Parse(i)).ToList();
        }

        public UserDTO Parse(User origin)
        {
            return origin == null ? new UserDTO() : new UserDTO() {
                id = origin.Id,
                name = origin.Name,
                last_name = origin.LastName,
                gender = origin.Gender,
                date_of_birth = origin.DateOfBirth,
                persist_date = origin.PersistDate,
                email = origin.Email,
                password = "",                
                type = origin.Type,
                last_access = origin.LastAccess
            };
        }

        public List<UserDTO> Parse(List<User> origin)
        {
            return origin.Select(i => Parse(i)).ToList();
        }
    }
}

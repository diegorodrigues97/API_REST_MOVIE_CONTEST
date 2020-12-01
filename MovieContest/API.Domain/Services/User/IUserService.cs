using MovieContest.Domain.DTO.Token;
using MovieContest.Domain.DTO.User;

namespace MovieContest.Domain.Services
{
    public interface IUserService
    {
        UserDTO GetById(long id);

        UserFilterDTO Get(UserFilterDTO filterDTO);

        UserDTO Add(UserDTO userDTO);

        UserDTO Edit(UserDTO userDTO);

        void Delete(long id);

        TokenDTO ValidateCredentials(UserDTO userDTO);

        TokenDTO RefreshToken(TokenDTO token);

        bool RevokeToken(string email);

        bool IsAdmin(string email);
    }
}

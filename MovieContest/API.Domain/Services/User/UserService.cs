using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MovieContest.Domain.Converter;
using MovieContest.Domain.DTO.Token;
using MovieContest.Domain.DTO.User;
using MovieContest.Domain.Entities;
using MovieContest.Domain.Entities.NotMapped;
using MovieContest.Domain.Model.Enumeration;

namespace MovieContest.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly ITokenGenerator _tokenService;
        private readonly UserConverter _converter;
        private readonly TokenConfiguration _configuration;
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public UserService(IGenericRepository<User> userRepository, 
            ITokenGenerator tokenService, 
            TokenConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _converter = new UserConverter();
            _configuration = configuration;

        }

        public UserDTO Add(UserDTO userDTO)
        {
            //Check if e-mail already exists
            if (string.IsNullOrEmpty(userDTO.email) || _userRepository.GetAll()
                .Any(u => u.Email.ToLower() == userDTO.email.ToLower() && u.FlagDeleted == false))
                throw new DomainException("E-mail já cadastrado!");            

            User user = _converter.Parse(userDTO);
            user.CheckIfIsValid(true);
            user.Password = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            user.FlagDeleted = false;
            user.PersistDate = DateTime.Now;

            return _converter.Parse(_userRepository.Create(user));
        }       

        public void Delete(long id)
        {
            var user = GetValidUser(id);

            user.FlagDeleted = true;
            _userRepository.Update(user);
        }

        public UserDTO Edit(UserDTO userDTO)
        {
            return _converter.Parse(_userRepository.Update(_converter.Parse(userDTO)));
        }

        public UserFilterDTO Get(UserFilterDTO filterDTO)
        {
            var query = _userRepository.GetAll().Where(u => u.FlagDeleted == false);

            //default
            int items_per_page = 10;
            int skip = items_per_page * (filterDTO.page - 1);

            filterDTO.items.Clear();
            filterDTO.items_per_page = items_per_page;
            filterDTO.total_items = query.Count();
            filterDTO.total_pages = (int)(filterDTO.total_items / items_per_page) 
                + ((filterDTO.total_items % items_per_page) > 0 ? 1 : 0);

            filterDTO.items = _converter.Parse(query.OrderBy(u => u.Name).OrderBy(u => u.LastName)
                .Take(items_per_page).Skip(skip).ToList());

            return filterDTO;
        }

        public UserDTO GetById(long id)
        {
            return _converter.Parse(_userRepository
                .GetAll().Where(u => u.Id == id && u.FlagDeleted == false).FirstOrDefault());
        }

        public TokenDTO ValidateCredentials(UserDTO userDTO)
        {
            string encryptesdPassword = ComputeHash(userDTO.password, new SHA256CryptoServiceProvider());

            var user = _userRepository.GetAll()
                .Where(u => u.Email.Equals(userDTO.email) && u.Password == encryptesdPassword)
                .FirstOrDefault();

            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _userRepository.Update(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenDTO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashBytes);
        }

        public TokenDTO RefreshToken(TokenDTO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            string email = principal.Identity.Name;

            var user = _userRepository.GetAll()
                .Where(u => u.Email == email && u.FlagDeleted == false).FirstOrDefault();

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryDate <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _userRepository.Update(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenDTO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string email)
        {
            var user = _userRepository.GetAll()
               .Where(u => u.Email == email && u.FlagDeleted == false).FirstOrDefault();

            if (user == null)
                return false;

            user.RefreshToken = null;
            _userRepository.Update(user);

            return true;
        }

        public bool IsAdmin(string email)
        {
            return _userRepository.GetAll()
                .Any(u => u.Email.ToLower() == email.ToLower() && u.FlagDeleted == false && u.Type == EUserType.ADMIN);
        }

        private User GetValidUser(long id, string email = "")
        {
            if (email == null)
                email = "";

            var user = _userRepository.GetAll()
                .Where(u => (u.Id == id || u.Email.ToLower() == email) && u.FlagDeleted == false).FirstOrDefault();

            if (user == null)
                throw new DomainException("Usuário não encontrado!");

            return user;
        }
    }
}

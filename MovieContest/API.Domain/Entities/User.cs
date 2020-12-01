using MovieContest.Domain.Helpers;
using MovieContest.Domain.Model.Enumeration;
using System;

namespace MovieContest.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }       
        public string LastName { get; set; }       
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime PersistDate { get; set; }  
        public string Email { get; set; }     
        public string Password { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public EUserType Type { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool FlagDeleted { get; set; }

        public void CheckIfIsValid(bool vPassword = false)
        {
            string errors = null;

            if (string.IsNullOrEmpty(Name) || Name.Length > 100)
                errors += "O NOME não pode ser nulo e deve conter no máximo 155 caracters!";
            if (string.IsNullOrEmpty(LastName) || Name.Length > 255)
                errors += "O SOBRENOME não pode ser nulo e deve conter no máximo 255 caracters!";
            if (string.IsNullOrEmpty(Email) || Name.Length > 200 || !Validate.Email(Email))
                errors += "O E-MAIL não é válido!";
            if (vPassword == true && !Validate.Password(Password))
                errors += "A SENHA deve possuir no mínimo 8 caracters!";

            if (errors != null)
                throw new DomainException(errors);
        }
    }
}

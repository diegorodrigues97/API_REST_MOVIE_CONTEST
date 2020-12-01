using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieContest.Domain.Helpers
{
    public class Validate
    {
        public static bool Email(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return rg.IsMatch(email);
        }

        public static bool Password(string pasword)
        {
            if (string.IsNullOrEmpty(pasword) || pasword.Length < 8)
                return false;

            return true;
        }
    }
}

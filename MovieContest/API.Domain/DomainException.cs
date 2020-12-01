using System;

namespace MovieContest.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string error) : base(error) { }
    }
}

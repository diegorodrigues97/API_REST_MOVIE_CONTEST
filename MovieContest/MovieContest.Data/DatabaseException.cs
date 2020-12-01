using System;

namespace MovieContest.Data
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string msg) : base(msg) { }
    }
}

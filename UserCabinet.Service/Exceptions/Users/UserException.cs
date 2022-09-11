
using System;

namespace UserCabinet.Service.Exceptions.Users
{
    public class UserException : Exception
    {
        public int Code { get; set; }

        public UserException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
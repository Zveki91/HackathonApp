using System;

namespace HackathonApp.Dto.Exceptions
{
    public class MyBadRequestException : Exception
    {
        public int Code { get; set; }

        public MyBadRequestException(string? message, int code) : base(message)
        {
            Code = code;
        }
    }
}
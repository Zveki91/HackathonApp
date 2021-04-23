using System;

namespace HackathonApp.Dto.Exceptions
{
    public class MyNotFoundException : Exception
    {
        public int Code { get; set; }

        public MyNotFoundException(string? message, int code) : base(message)
        {
            Code = code;
        }
    }
}
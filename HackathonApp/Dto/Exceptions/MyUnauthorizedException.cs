using System;

namespace HackathonApp.Dto.Exceptions
{
    public abstract class MyUnauthorizedException : Exception
    {
        private int Code { get; set; }

        protected MyUnauthorizedException(string? message, int code) : base(message)
        {
            Code = code;
        }
    }
}
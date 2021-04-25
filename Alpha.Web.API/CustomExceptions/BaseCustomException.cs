using System;
using System.Net;

namespace Alpha.Web.API.CustomExceptions
{
    /// <summary>
    /// This class is a father of anothers custom exeptions implementations
    /// </summary>
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string message, string description, HttpStatusCode code) : base(message)
        {
            Code = code;
            Description = description;
        }

        public HttpStatusCode Code { get; set; }
        public string Description { get; set; }
    }
}


using System.Net;

namespace Alpha.Web.API.CustomExceptions
{
    /// <summary>
    /// This class implements a custom NotFound exception
    /// </summary>
    public class NotFoundCustomException : BaseCustomException
    {
        public NotFoundCustomException(string message) : base(message, string.Empty, HttpStatusCode.NotFound)
        {
        }

        public NotFoundCustomException(string message, string description) : base(message, description,
            HttpStatusCode.NotFound)
        {
        }
    }
}

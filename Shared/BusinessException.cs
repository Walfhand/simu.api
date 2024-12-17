using System.Net;

namespace Simu.Api.Shared;

public abstract class BusinessException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : Exception(message)
{
    public HttpStatusCode StatusCode => statusCode;
}
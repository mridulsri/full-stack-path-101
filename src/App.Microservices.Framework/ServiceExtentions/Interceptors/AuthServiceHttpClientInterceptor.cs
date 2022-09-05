using System.Net;

namespace App.Microservices.Framework.ServiceExtentions.Interceptors;

public class AuthServiceHttpClientInterceptor:DelegatingHandler
{
    public AuthServiceHttpClientInterceptor()
    {

    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var respnse =  await base.SendAsync(request, cancellationToken);
        if(respnse.StatusCode.Equals(HttpStatusCode.Unauthorized))
        {
            //get token and update the header.

            request.Headers.Add("Authorization", "");
        }
        return await base.SendAsync(request, cancellationToken);
    }
}

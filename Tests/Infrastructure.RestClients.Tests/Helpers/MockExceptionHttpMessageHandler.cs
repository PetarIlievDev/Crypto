namespace Infrastructure.RestClients.Tests.Helpers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class MockExceptionHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly object? _responseContent;

        public MockExceptionHttpMessageHandler(HttpStatusCode statusCode, object? responseContent = null)
        {
            _statusCode = statusCode;
            _responseContent = responseContent;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new Exception("Exception thrown for testing purposes");
        }
    }
}

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace TurtleSec.Functions
{
    public class HttpExample
    {
        private readonly ILogger _logger;

        public HttpExample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpExample>();
        }

        [Function("HttpExample")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functionz!");

            return response;
        }

        // [Function("decorator")]
        // public HttpResponseData Deco2([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        // {
        //     _logger.LogInformation("C# HTTP trigger function processed a request.");

        //     var response = req.CreateResponse(HttpStatusCode.OK);
        //     response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        //     response.WriteString("Its decorative!");

        //     return response;
        // }
    }
}
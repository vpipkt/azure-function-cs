using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace TurtleSec.Functions
{
    public class MultiResponse
    {
        [QueueOutput("outqueue",Connection = "AzureWebJobsStorage")]
        public string[] Messages { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
    
    public class HttpExample 
    {      

        private readonly ILogger _logger;

        public HttpExample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpExample>();
        }

        [Function("HttpExample")]
        public static MultiResponse Multi([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpExample");
            logger.LogInformation("C# HTTP trigger function processed a request... HttpExample");

            var message = "Welcome to functions /HttpExample";

            if (req.Method == "POST")
            {
                logger.LogInformation("Consuming request body...");
                using (StreamReader sr = new StreamReader(req.Body))
                {
                    var messages = sr.ReadToEnd();
                    message = String.Join("", messages);

                }
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(message);

            // Return a response to both HTTP trigger and storage output binding.
            return new MultiResponse()
            {
                // Write a single message.
                Messages = new string[] { message },
                HttpResponse = response
            };
        }


        [Function("hello")]
        public HttpResponseData Hello([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a " + req.Method + " request at /hello.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functionz!");

            return response;
        }

        [Function("decorator")]
        public HttpResponseData Deco2([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request at /decorator.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Its decorative!\nv0.0.1");

            return response;
        }
    }
}

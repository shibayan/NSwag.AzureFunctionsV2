using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using NSwag.Annotations;
using NSwag.Annotations.AzureFunctions;
using NSwag.SwaggerGeneration.AzureFunctionsV2.Tests.TestFunctionApp.Helpers;

namespace NSwag.SwaggerGeneration.AzureFunctionsV2.Tests.TestFunctionApp
{
    public static class Functions1
    {
        [FunctionName("DefaultNoParamsNoAnnotations")]
        public static async Task<IActionResult> DefaultNoParamsNoAnnotations([HttpTrigger(
            AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            return new OkObjectResult("ok");
        }

        [FunctionName("PathParameters")]
        public static async Task<IActionResult> PathParameters([HttpTrigger(
            AuthorizationLevel.Anonymous, "GET", Route = "test/{stringparam}/{intparam}")] HttpRequest req,
            string stringparam, int intparam,
            ILogger log)
        {
            log.LogInformation("Triggered");
            return new OkObjectResult($"stringparam: {stringparam}, intparam: {intparam}");
        }

        [SwaggerResponse(200, typeof(ResponseModelWithPrimitives), Description = "Description", IsNullable = false)]
        [FunctionName("ReturnValueAnnotation")]
        public static async Task<IActionResult> ReturnValueAnnotation([HttpTrigger(
            AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            return new OkObjectResult(new ResponseModelWithPrimitives() { IntValue = 1, StringValue = "hello swagger" });
        }

        [OpenApiRequestBodyType(typeof(string), true, "RequestPrimitive", "Description")]
        [FunctionName("PostPrimitiveTypeAnnotation")]
        public static async Task<IActionResult> PostPrimitiveTypeAnnotation([HttpTrigger(
                AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            var input = await req.ReadAsStringAsync();
            return new OkObjectResult(input);
        }

        [OpenApiRequestBodyType(typeof(RequestBodyModelWithPrimitives), true, "RequestModel", "Description")]
        [FunctionName("PostComplexTypeAnnotation")]
        public static async Task<IActionResult> PostComplexTypeAnnotation([HttpTrigger(
                AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            var input = JsonConvert.DeserializeObject<RequestBodyModelWithPrimitives>(await req.ReadAsStringAsync());
            return new OkObjectResult(input);
        }

        [OpenApiRequestBodyType(typeof(RequestBodyModelWithComplexType), true, "RequestModel", "Description")]
        [FunctionName("PostNestedComplexTypeAnnotation")]
        public static async Task<IActionResult> PostNestedComplexTypeAnnotation([HttpTrigger(
                AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            var input = JsonConvert.DeserializeObject<RequestBodyModelWithComplexType>(await req.ReadAsStringAsync());
            return new OkObjectResult(input);
        }

        [OpenApiFormDataFile(true, "file", "Description")]
        [OpenApiFormData("text", false, typeof(string), "Description")]
        [OpenApiFormData("number", false, typeof(int), "Description")]
        [Consumes("multipart/form-data")]
        [FunctionName("MultipartFormUploadAnnotation")]
        public static async Task<IActionResult> MultipartFormUploadAnnotation([HttpTrigger(
            AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");

            var formData = await MultipartFormReader.Read(req);
            return new OkObjectResult(formData);

        }

        [FunctionName("AuthorizeAnnotation")]
        [OpenApiAuthorize(AuthScheme.Basic)]
        public static async Task<IActionResult> AuthorizeAnnotation([HttpTrigger(
            AuthorizationLevel.Anonymous, "GET", Route = "test/authorizedonly")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Triggered");
            return new OkObjectResult("ok");
        }


    }
}

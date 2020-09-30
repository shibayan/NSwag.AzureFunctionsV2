using System.Linq;
using System.Reflection;

using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NSwag.Generation.AzureFunctions.Processors
{
    /// <summary>
    /// Processes operation response annotations/attributes into the swagger document.
    /// </summary>
    public class OperationResponseProcessor : OperationResponseProcessorBase, IOperationProcessor
    {

        public OperationResponseProcessor(AzureFunctionsOpenApiDocumentGeneratorSettings settings) 
            : base(settings)
        {
        }

        public bool Process(OperationProcessorContext context)
        {
            var responseTypeAttributes = context.MethodInfo.GetCustomAttributes()
                .Where(a => a.GetType().Name == "ResponseTypeAttribute" ||
                            a.GetType().Name == "SwaggerResponseAttribute")
                .ToList();

            var producesResponseTypeAttributes = context.MethodInfo.GetCustomAttributes()
                .Where(a => a.GetType().Name == "ProducesResponseTypeAttribute" ||
                            a.GetType().Name == "ProducesAttribute")
                .ToList();

            var attributes = responseTypeAttributes.Concat(producesResponseTypeAttributes);

            ProcessResponseTypeAttributes(context, attributes);

            return true;
        }

        protected override string GetVoidResponseStatusCode()
        {
            return "204";
        }
    }
}
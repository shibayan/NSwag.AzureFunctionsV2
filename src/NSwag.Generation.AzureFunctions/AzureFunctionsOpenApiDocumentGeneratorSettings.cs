using NSwag.Generation.AzureFunctions.Processors;
using NSwag.Generation.Processors;

namespace NSwag.Generation.AzureFunctions
{
    /// <summary>
    /// The SwaggerGenerator settings.
    /// <para>
    /// The default settings set up <see cref="OperationParameterProcessor"/> and <see cref="OperationResponseProcessor"/>.
    /// Security processors must be added manually matching your security usage (Basic, OAuth2, ApiKey...).
    /// </para>
    /// </summary>
    public class AzureFunctionsOpenApiDocumentGeneratorSettings : OpenApiDocumentGeneratorSettings
    {
        /// <summary>Initializes a new instance of the <see cref="AzureFunctionsOpenApiDocumentGeneratorSettings"/> class.</summary>
        public AzureFunctionsOpenApiDocumentGeneratorSettings()
        {
            OperationProcessors.Insert(0, new ApiVersionProcessor());
            OperationProcessors.Insert(3, new OperationParameterProcessor(this));
            OperationProcessors.Insert(3, new OperationResponseProcessor(this));
        }

        /// <summary>Gets or sets a value indicating whether to add path parameters which are missing in the action method.</summary>
        public bool AddMissingPathParameters { get; set; }

        /// <summary>
        /// Gets or sets the RoutePrefix that can be defined in host.json. By default it is "api", if you override this in your host.json, you should set the same value here.
        /// </summary>
        public string RoutePrefix { get; set; }
    }
}
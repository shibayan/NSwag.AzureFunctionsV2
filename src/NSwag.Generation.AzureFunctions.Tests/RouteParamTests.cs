using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using NJsonSchema;

using Xunit;

namespace NSwag.Generation.AzureFunctions.Tests
{
    public class RouteParamTests
    {
        [Fact]
        public async Task Should_include_two_route_params_from_function_signature()
        {
            // Arrange
            var settings = new AzureFunctionsOpenApiDocumentGeneratorSettings();
            var generator = new AzureFunctionsOpenApiDocumentGenerator(settings);
            var functionName = nameof(SwaggerGeneration.AzureFunctionsV2.Tests.HttpExtensionsTestApp.RouteParamTests.RouteParamTest);

            // Act
            var swaggerDoc = await generator.GenerateForAzureFunctionClassAndSpecificMethodsAsync(
                typeof(SwaggerGeneration.AzureFunctionsV2.Tests.HttpExtensionsTestApp.RouteParamTests), new List<string>() { functionName });

            // Assert
            var operation = swaggerDoc.Operations.First().Operation;
            operation.ActualParameters.Count.Should().Be(2);
            operation.ActualParameters[0].Kind.Should().Be(OpenApiParameterKind.Path);
            operation.ActualParameters[1].Kind.Should().Be(OpenApiParameterKind.Path);
            operation.ActualParameters[0].IsRequired.Should().Be(true);
            operation.ActualParameters[1].IsRequired.Should().Be(true);
            operation.ActualParameters[0].Type.Should().Be(JsonObjectType.Integer);
            operation.ActualParameters[1].Type.Should().Be(JsonObjectType.String);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

using NSwag.Generation.AzureFunctions;
using NSwag.SwaggerGeneration.AzureFunctionsV2.Tests.TestFunctionApp;
using Xunit;

namespace NSwag.SwaggerGeneration.AzureFunctionsV2.Tests
{
    public class NonStaticFunctionClassTests
    {
        [Fact]
        public async Task Should_include_nonstatic_function_classes_and_methods()
        {
            // Arrange
            var settings = new AzureFunctionsOpenApiDocumentGeneratorSettings();
            var generator = new AzureFunctionsOpenApiDocumentGenerator(settings);

            // Act
            var swaggerDoc = await generator.GenerateForAzureFunctionClassesAsync(
                new[] { typeof(NonStaticFunctionClass), typeof(IgnoredNonStaticFunctionClass) }, null);

            // Assert
            swaggerDoc.Operations.Count().Should().Be(1);
        }
    }
}

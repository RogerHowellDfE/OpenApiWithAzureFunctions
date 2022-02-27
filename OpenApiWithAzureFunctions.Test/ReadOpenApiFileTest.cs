using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenApiWithAzureFunctions.OpenApi;

namespace OpenApiWithAzureFunctions.Test
{

    [TestClass]
    public class ReadOpenApiFileTest
    {
        private const string FilePath = @"C:\dev\repos\OpenApiWithAzureFunctions\OpenApiWithAzureFunctions.Test\TestData\Texuna_dictionary-lookups_minimal.yaml";


        [TestMethod]
        public async Task GivenOpenApiFileParsedToCSharp_WhenConvertedToYaml_ThenSameStringReturned()
        {
            // Arrange
            var foo = new OpenApiHelper();
            var openApiDocument = await foo.fetchAsync(FilePath);


            // Act
            var outputString = openApiDocument.Serialize(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Yaml);


            // Assert
            var expected = @"swagger: '2.0'
info:
  title: Texuna Edubase API
  description: The Department for Education's register of educational establishments in England and Wales.
  version: 1.0.0
host: edubase.texunatech.com
basePath: /v1
schemes:
  - https
paths:
  /lookup/governor-roles:
    get:
      tags:
        - Lookup
      summary: Retrieve all the Governor Roles
      produces:
        - application/json
      responses:
        '200':
          description: An array of LookupResult
          schema:
            type: array
            items:
              $ref: '#/definitions/LookupResult'
        default:
          description: Unexpected error
          schema:
            $ref: '#/definitions/Error'
definitions:
  LookupResult:
    description: Model for returning lookup values from the lookup endpoints.
    type: object
    properties:
      id:
        format: Int32
        description: identifier
        type: integer
      name:
        description: display text
        type: string
      displayOrder:
        format: Int32
        description: order of the item (if applicable)
        type: integer
      code:
        description: the code or number representation of the item
        type: string
  Error:
    type: object
    properties:
      code:
        format: int32
        type: integer
      message:
        type: string
      fields:
        type: string";


            Assert.AreEqual(
                normaliseLineEndings(expected),
                normaliseLineEndings(outputString)
            );

        }



        private static string normaliseLineEndings(string input)
        {
            return Regex.Replace(input, @"\r\n|\n\r|\n|\r", "\r\n");
        }
    }

}

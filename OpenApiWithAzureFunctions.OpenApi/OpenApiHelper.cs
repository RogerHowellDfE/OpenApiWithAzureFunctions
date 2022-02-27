using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;

namespace OpenApiWithAzureFunctions.OpenApi
{
    public class OpenApiHelper
    {


        public  async Task<Microsoft.OpenApi.Models.OpenApiDocument> fetchAsync(string filePath)
        {

            //var httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/")
            //};

            //var openApiInputStream = await httpClient.GetStreamAsync("master/examples/v3.0/petstore.yaml");


            //string openApiInput = System.IO.File.ReadAllText(fileName);

            using FileStream openApiInputStream = File.OpenRead(filePath);


            // Read V3 as YAML
            var openApiDocument = new OpenApiStreamReader().Read(openApiInputStream, out var diagnostic);
            Console.WriteLine(diagnostic);

            return openApiDocument;

        }

    }
}

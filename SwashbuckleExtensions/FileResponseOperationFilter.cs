using System;
using System.Linq;
using System.Net;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public class FileResponseOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var responseAttributes = context.ApiDescription.ActionAttributes()
                .OfType<SwaggerFileResponseAttribute>()
                .OrderBy(attr => attr.StatusCode);

            foreach (var attr in responseAttributes)
            {
                var statusCode = attr.StatusCode.ToString();

                Schema responseSchema = new Schema {Type = "file"};

                operation.Produces.Add(attr.MimeType);
                operation.Responses[statusCode] = new Response
                {
                    Description = attr.Description ?? InferDescriptionFrom(statusCode),
                    Schema = responseSchema
                };
            }
        }

        private string InferDescriptionFrom(string statusCode)
        {
            return Enum.TryParse(statusCode, true, out HttpStatusCode enumValue)
                ? enumValue.ToString()
                : null;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public class DateTimeRequiredDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var defs = swaggerDoc.Definitions
                .Select(definition => new
                {
                    Definition = definition.Value,
                    DateTimeProperties = definition.Value.Properties
                        .Where(prop => prop.Value.Format == "date-time"
                                       && prop.Value.Extensions != null
                                       && prop.Value.Extensions
                                           .Any(ext => ext.Key == "x-nullable"
                                                       && (bool) ext.Value == false))
                })
                .Where(def => def.DateTimeProperties.Any());

            foreach (var def in defs)
            {
                if(def.Definition.Required == null)
                    def.Definition.Required = new List<string>();

                def.Definition.Required = def.Definition.Required
                    .Union(def.DateTimeProperties.Select(prop => prop.Key))
                    .ToList();
            }
        }
    }
}
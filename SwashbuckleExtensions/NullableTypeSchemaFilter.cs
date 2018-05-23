using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public class NullableTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            Type type = context.SystemType;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                type = type.GetGenericArguments()[1];
            }

            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);

            if (nullableUnderlyingType == null && type.IsValueType)
                model.Extensions.Add("x-nullable", false);
        }
    }
}

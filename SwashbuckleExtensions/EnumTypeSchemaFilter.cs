using System;
using System.Collections.Generic;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public class EnumTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var type = context.SystemType;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                type = underlyingType;
            }

            if (type.IsEnum)
            {
                GenerateEnum(schema, type);
            }
        }

        private void GenerateEnum(Schema schema, Type type)
        {
            schema.Type = "string";
            schema.Format = null;
            schema.Extensions.Add(
                "x-ms-enum",
                new
                {
                    name = type.Name,
                    modelAsString = false,
                    values = GetEnumValues(type)
                }
            );
        }

        private IList<EnumValueDescriptor> GetEnumValues(Type enumType)
        {
            var enumValues = enumType
                .GetFields(BindingFlags.Static | BindingFlags.Public);

            IList<EnumValueDescriptor> values = new List<EnumValueDescriptor>();
            foreach (FieldInfo enumValue in enumValues)
            {
                int value = (int)enumValue.GetValue(null);
                EnumValueDescriptor descriptor = new EnumValueDescriptor
                {
                    value = value,
                    name = Enum.GetName(enumType, value)
                };

                values.Add(descriptor);
            }

            return values;
        }

        private class EnumValueDescriptor
        {
            public int value { get; set; }
            public string name { get; set; }
        }

    }
}
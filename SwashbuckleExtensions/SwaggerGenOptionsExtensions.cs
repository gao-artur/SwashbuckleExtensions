using System;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void ApplyCorrelationIdHeaderOperationFilter(this SwaggerGenOptions options)
        {
            options.OperationFilter<CorrelationIdHeaderOperationFilter>();
        }

        public static void ApplyFileResponseOperationFilter(this SwaggerGenOptions options)
        {
            options.OperationFilter<FileResponseOperationFilter>();
        }
        
        public static void ApplyNullableTypeSchemaFilter(this SwaggerGenOptions options)
        {
            options.SchemaFilter<NullableTypeSchemaFilter>();
        }

        /// <summary>
        /// Be sure you don't use DescribeAllEnumsAsStrings method with ApplyEnumTypeSchemaFilter
        /// </summary>
        /// <param name="options"></param>
        public static void ApplyEnumTypeSchemaFilter(this SwaggerGenOptions options)
        {
            options.SchemaFilter<EnumTypeSchemaFilter>();
        }

        /// <summary>
        /// Why to use:
        /// When generating client with autorest, it creates models with constructor with all arguments as optional
        /// and initializes them with default values. If such constructor has argument initializer 
        /// for DateTime (e.g. DateTime start = default(DateTime)) then when using automapper 
        /// to map some model to such auto-generated model the following exception thrown:
        ///  "Encountered an invalid type for a default value".
        /// 
        /// This is coreclr bug that has been recently fixed and merged (04.05.18). Once new version deployed
        /// this can be obsolete.
        /// 
        /// PR: https://github.com/dotnet/coreclr/pull/17877
        /// </summary>
        public static void MakeDateTimePropertiesRequired(this SwaggerGenOptions options)
        {
            options.DocumentFilter<DateTimeRequiredDocumentFilter>();
        }

        public static void MapGuids(this SwaggerGenOptions options)
        {
            options.MapType<Guid>(() => new Schema { Type = "string", Format = "uuid", Example = Guid.Empty });
            options.MapType<Guid?>(() => new Schema { Type = "string", Format = "uuid", Example = Guid.Empty });
        }
    }
}
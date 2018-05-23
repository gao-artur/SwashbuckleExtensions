using System;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddHeaderToAllEndpoints(this SwaggerGenOptions options, string headerName, object defaultValue, bool required = false)
        {
            options.OperationFilter<HeaderOperationFilter>(headerName, defaultValue, required);
        }

        public static void ApplyFileResponseOperationFilter(this SwaggerGenOptions options)
        {
            options.OperationFilter<FileResponseOperationFilter>();
        }

        /// <summary>
        /// Add 'x-nullable=false' extension to all nit nullable value types.
        /// </summary>
        public static void MakeValueTypesNotNullable(this SwaggerGenOptions options)
        {
            options.SchemaFilter<NullableTypeSchemaFilter>();
        }

        /// <summary>
        /// Generate enum types instead of strings or integers.
        /// Be sure you don't use DescribeAllEnumsAsStrings method with GenerateEnums.
        /// </summary>
        /// <param name="options"></param>
        public static void GenerateEnums(this SwaggerGenOptions options)
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

        /// <summary>
        /// Set empty guid as example for guid types in SwaggerUI.
        /// </summary>
        public static void MapGuids(this SwaggerGenOptions options)
        {
            options.MapType<Guid>(() => new Schema { Type = "string", Format = "uuid", Example = Guid.Empty });
            options.MapType<Guid?>(() => new Schema { Type = "string", Format = "uuid", Example = Guid.Empty });
        }
    }
}
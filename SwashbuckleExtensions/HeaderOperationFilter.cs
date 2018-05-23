using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    public class HeaderOperationFilter : IOperationFilter
    {
        private readonly string _headerName;
        private readonly object _defaultValue;
        private readonly bool _required;

        public HeaderOperationFilter(string headerName, object defaultValue, bool required = false)
        {
            _headerName = headerName;
            _defaultValue = defaultValue;
            _required = required;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if(operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = _headerName,
                In = "header",
                Type = "string",
                Required = _required,
                Default = _defaultValue
            });
        }
    }
}
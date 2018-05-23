using System;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class SwaggerFileResponseAttribute : SwaggerResponseAttribute
    {
        public string MimeType { get; }

        public SwaggerFileResponseAttribute(int statusCode, string mimeType) : this(statusCode, mimeType, null)
        {
        }

        public SwaggerFileResponseAttribute(int statusCode, string mimeType, string description = null)
            : base(statusCode, description: description)
        {
            MimeType = mimeType;
        }
    }
}
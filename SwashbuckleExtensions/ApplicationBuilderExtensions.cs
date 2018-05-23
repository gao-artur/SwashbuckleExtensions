using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleExtensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// When using service behind ngnix SwaggerUi will be initiated with wrong swagger.json url.
        /// You can use this extension instead of manually add Swagger and SwaggerUI to overcome this problem.
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerWithControllerRoute<T>(this IApplicationBuilder builder,
            string description) where T : Controller
        {
            Type type = typeof(T);

            string controllerName = type.Name;
            int controllerIndex = controllerName.LastIndexOf("Controller", StringComparison.Ordinal);
            int lenght = controllerIndex != -1 ? controllerIndex : controllerName.Length;

            string name = controllerName.Substring(0, lenght);

            builder.UseSwagger(c =>
                {
                    c.RouteTemplate = $"api/{name}/{{documentName}}/swagger.json";
                })
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/api/{name}/v1/swagger.json", description);
                });

            return builder;
        }
    }
}
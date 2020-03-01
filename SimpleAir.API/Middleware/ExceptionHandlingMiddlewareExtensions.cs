using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace SimpleAir.API.Middleware
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(
           this IApplicationBuilder builder, string product, string layer,
           string errorHandlingPath)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleWare>
                (product, layer, Options.Create(new ExceptionHandlerOptions
                {
                    ExceptionHandlingPath = new PathString(errorHandlingPath)
                }));
        }
    }
}
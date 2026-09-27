namespace SalesDB.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimeMiddleware>()
                    .UseMiddleware<BlockIpMiddleware>();
    }
}
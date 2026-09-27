using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using SalesDB.Dtos.Base;

namespace SalesDB.Middlewares;

public static class GlobalExceptionMiddleware
{
    public static void UseGlobalExceptionHandle(this WebApplication app)
    {
        app.UseExceptionHandler(err =>
        {
            err.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var statusCode = exception switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError

                };
                var message = exception switch
                {
                    ArgumentException => exception.Message,
                    KeyNotFoundException => exception.Message,
                    UnauthorizedAccessException => "Không có quyền truy cập",
                    _ => "Đã xảy ra lỗi"
                };
                // set giá trị cho response
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";


                // arr chỉ có 3 phần tử , truy cập phânt tu 10 => 

                var res = new ResponseEntity(statusCode, null, message);
                await context.Response.WriteAsJsonAsync(res);
            });
        });
    }
}

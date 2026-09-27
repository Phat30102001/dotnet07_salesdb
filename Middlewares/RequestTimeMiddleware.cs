using System.Diagnostics;

namespace SalesDB.Middlewares;

// xử lý tổng thời gian request từ lúc bắt đầu đến khi nhận được response và kết thúc
public class RequestTimeMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Bat dau tinh gio 
        var stopWatch = Stopwatch.StartNew();
        context.Response.OnStarting(() =>
        {
            stopWatch.Stop();
            context.Response.Headers["X-Responxe-Time"] = stopWatch.ElapsedMilliseconds.ToString();
            Console.WriteLine("🟢 [TIME] - " + stopWatch.ElapsedMilliseconds.ToString());
            return Task.CompletedTask;
        });
        
        await next(context);
    }
}

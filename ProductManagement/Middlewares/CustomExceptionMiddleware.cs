using System.Net;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProductManagement.Middlewares.Errors;

namespace ProductManagement.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred."); // Hataları logla
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is DbUpdateException dbEx && dbEx.InnerException is PostgresException pgEx)
        {
            _logger.LogError(pgEx, "Postgres-specific database error occurred."); // Postgres hatasını logla

            if (pgEx.SqlState == "23503") // Foreign key constraint violation
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Foreign key constraint violation. Please check your data."
                }.ToString());
            }
        }
        
        // Diğer hatalar için loglama
        _logger.LogError(exception, "An unexpected error occurred."); // Genel hata loglama

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "An unexpected error occurred. Please try again later."
        }.ToString());
    }
}

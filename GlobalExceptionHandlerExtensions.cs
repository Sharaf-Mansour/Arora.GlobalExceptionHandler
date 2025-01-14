using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace Arora.GlobalExceptionHandler;
/// <summary>
/// Provides extension methods for configuring global exception handling in a WebApplication.
/// </summary>
public static class GlobalExceptionHandlerExtensions
{
    /// <summary>
    /// Adds global exception handling middleware to the application's request pipeline.
    /// Catches all unhandled exceptions and returns a JSON response with the exception message.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <returns>The configured WebApplication with global exception handling enabled.</returns>
    public static WebApplication UseGlobalExceptionHandler(this WebApplication app)
    {
        /// <summary>
        /// Handles the exception by writing a JSON response with the exception message.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="ex">The caught exception.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Serialize the exception message as JSON
            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = ex.Message
            }));
        }

        // Add middleware to handle exceptions in the request pipeline
        app.Use(async (context, next) =>
        {
            try
            {
                await next(); // Proceed with the next middleware
            }
            catch (Exception ex)
            {
                // Catch unhandled exceptions and pass to the handler
                await HandleExceptionAsync(context, ex);
            }
        });

        return app;
    }
}

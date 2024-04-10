using CompanyName.DDD.Domain.Common.Log;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text.Json;
namespace CompanyName.DDD.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _environment;
        private readonly ILog _log;
        public ExceptionMiddleware(RequestDelegate next, ILog log)
        {
            _next = next;
            _log = log;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
        }
        private static Task HandleGlobalExceptionAsync(HttpContext context, int StatusCode, string message, string error, string? innerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCode;
            var result = new { message, error, innerError };
            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Info", $"LLamada a controlador {context.Request.Path} correcta", ""));
            }
            catch (SqlException ex)
            {
                var customMessage = "Ha ocurrido un error no controlado con la base de datos";
                if (ex.Number.Equals(53)) await HandleGlobalExceptionAsync(context, 500, "Se ha generado un error al intentar conectar con la base de datos", ex.Message, ex.InnerException?.Message);
                if (ex.Number.Equals(-2)) await HandleGlobalExceptionAsync(context, 408, "Se ha superado el tiempo de espera con la base de datos", ex.Message, ex.InnerException?.Message);
                else await HandleGlobalExceptionAsync(context, 500, customMessage, ex.Message, ex.InnerException?.Message);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Error", customMessage, new { message = ex.Message, innerException = ex.InnerException?.Message }));
            }
            catch (DbUpdateException ex)
            {
                var customMessage = "Se ha generado un error de consistencia en la base de datos";
                await HandleGlobalExceptionAsync(context, 500, customMessage, ex.Message, ex.InnerException?.Message);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Error", customMessage, new { message = ex.Message, innerException = ex.InnerException?.Message }));
            }
            catch (WarningException ex)
            {
                await HandleGlobalExceptionAsync(context, 400, ex.Message, ex.Message, ex.InnerException?.Message);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Error", ex.Message, new { message = ex.Message, innerException = ex.InnerException?.Message }));
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                var customMessage = "No tiene acceso para acceder a este recurso";
                await HandleGlobalExceptionAsync(context, 403, customMessage, ex.Message, ex.InnerException?.Message);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Error", customMessage, new { message = ex.Message, innerException = ex.InnerException?.Message }));
            }
            catch (Exception ex)
            {
                var customMessage = "Ha ocurrido un error no controlado con la aplicación";
                await HandleGlobalExceptionAsync(context, 500, customMessage, ex.Message, ex.InnerException?.Message);
                _log.Send(new BodyLog(Guid.NewGuid(), _environment, "Error", customMessage, new { message = ex.Message, innerException = ex.InnerException?.Message }));
            }
        }
    }
}
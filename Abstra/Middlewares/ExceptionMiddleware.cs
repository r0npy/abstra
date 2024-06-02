using Abstra.Core.Exceptions;
using Abstra.Mappers.Responses;
using NLog;
using System.Net;
using System.Text.Json;

namespace Abstra.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment)
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (BussinessValidationException fex)
            {
                _logger.Error(fex);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                BussinessExceptionResponseDto response = new(httpContext.Response.StatusCode, fex.Message,
                    hostEnvironment.IsDevelopment() ?
                    fex.StackTrace?.ToString() : null);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (BussinessException fex)
            {
                _logger.Error(fex);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                BussinessExceptionResponseDto response = new(httpContext.Response.StatusCode, fex.Message,
                    hostEnvironment.IsDevelopment() ?
                    fex.StackTrace?.ToString() : null);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                BussinessExceptionResponseDto response = new(httpContext.Response.StatusCode,
                    hostEnvironment.IsDevelopment() ? ex.Message : "Error Interno del Servidor",
                    hostEnvironment.IsDevelopment() ? ex.StackTrace?.ToString() : null);


                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

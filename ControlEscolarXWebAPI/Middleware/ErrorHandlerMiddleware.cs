using Application.Responses;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;


namespace ControlEscolarXWebAPI.Middleware
{
    /// <summary>
    /// Middleware para manejar errores globales en la aplicación.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoca el middleware para manejar errores en la solicitud HTTP.
        /// </summary>
        /// <param name="context">El contexto de la solicitud HTTP.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var respuesta = context.Response;

                var respuestaModelo = new APIReply<string>
                {
                    message = error.Message,
                };

                switch (error)
                {
                    default:
                        // Maneja cualquier otra excepción no específica
                        respuestaModelo.result = "Exception error";
                        respuestaModelo.statusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                var resultado = JsonSerializer.Serialize(respuestaModelo);
                respuesta.ContentType = "application/json";
                respuesta.StatusCode = (int)respuestaModelo.statusCode;
                await respuesta.WriteAsync(resultado);
            }
        }
    }
}

using FluentValidation;
using LoanMe.Shared.Models;
using System.Text.Json;

namespace LoanMe.Api.Middlewares {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch (ValidationException ex) {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errorObject = Result<object>
                    .MultipleErrors(ex.Errors.Select(err => err.ErrorMessage));

                var serializedErrorObj = JsonSerializer.Serialize(errorObject, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsJsonAsync(serializedErrorObj);
            }
            catch (Exception ex) {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorObject = Result<object>
                    .Error("Something went wrong.");

                await context.Response.WriteAsJsonAsync(errorObject);
            }
        }
    }
}

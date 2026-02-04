using FluentValidation;
using LoanMe.Shared.Models;
using System.Text.Json;

namespace LoanMe.Api.Middlewares {
    public class ExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch (ValidationException ex) {
                //context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errorObject = Result<object>
                    .MultipleErrors(ex.Errors.Select(err => err.ErrorMessage));

                await context.Response.WriteAsJsonAsync(errorObject);
            }
            catch (Exception ex) {
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorObject = Result<object>
                    .Error("Something went wrong.");

                await context.Response.WriteAsJsonAsync(errorObject);
            }
        }
    }
}

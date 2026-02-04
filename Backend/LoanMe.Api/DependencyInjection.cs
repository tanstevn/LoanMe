using FluentValidation;
using LoanMe.Api.Middlewares;
using LoanMe.Application;
using LoanMe.Data;
using LoanMe.Infrastructure.Mediator;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace LoanMe.Api {
    public static class DependencyInjection {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config) {
            services.AddOpenApi();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddMediatorFromAssembly(typeof(MediatorAnchor).Assembly);
            services.AddValidatorsFromAssembly(typeof(MediatorAnchor).Assembly);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("LoanMe")));

            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        [SuppressMessage("Usage", "ASP0014:Suggest using top level route registrations", Justification = "<Pending>")]
        public static void ConfigureApplication(WebApplication app, IWebHostEnvironment env) {
            if (!env.IsEnvironment("Production")) {
                app.MapOpenApi();
            }

            app.UseCors();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapFallback(context => {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    return context
                        .Response
                        .WriteAsJsonAsync(string.Empty);
                });
            });

            using var scope = app.Services
                .CreateScope();

            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>()
                .Database
                .Migrate();
        }
    }
}

using LoanMe.Infrastructure.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LoanMe.Infrastructure.Mediator {
    public static class Extensions {
        public static void AddMediatorFromAssembly(this IServiceCollection services, Assembly assembly) {
            var assemblyTypes = assembly.GetTypes();

            services.AddScoped<IMediator, Mediator>();
            services.AddRequestHandlers(assemblyTypes);
        }

        private static void AddRequestHandlers(this IServiceCollection services, Type[] types) {
            var handlerTypes = types
                .Where(type => type.IsClass && !type.IsAbstract)
                .SelectMany(type => type.GetInterfaces()
                    .Where(@interface => @interface.IsGenericType
                        && @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)),
                    (type, @interface) => new {
                        Interface = @interface,
                        Implementation = type
                    });

            foreach (var type in handlerTypes) {
                // Registering the Request Handler(s) as Open-Generic (e.g., typeof(IRequestHandler<,>))
                services.AddTransient(type.Interface, type.Implementation);
            }
        }
    }
}

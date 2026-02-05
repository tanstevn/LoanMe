using LoanMe.Infrastructure.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LoanMe.Infrastructure.Mediator {
    public class Executor<TRequest, TResponse> : IExecutor 
        where TRequest : IRequest<TResponse> {
        public async Task<object> ExecuteAsync(object request, IServiceProvider serviceProvider) {
            var requestHandler = serviceProvider
                .GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            ArgumentNullException.ThrowIfNull(requestHandler);

            return (await requestHandler.HandleAsync((TRequest)request))!;
        }
    }
}

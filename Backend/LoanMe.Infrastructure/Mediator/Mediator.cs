using LoanMe.Infrastructure.Mediator.Abstractions;

namespace LoanMe.Infrastructure.Mediator {
    public class Mediator : IMediator {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) {
            ArgumentNullException.ThrowIfNull(request);

            var executorType = typeof(Executor<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var requestHandler = (IExecutor)Activator.CreateInstance(executorType)!;

            var result = await requestHandler.ExecuteAsync(request, _serviceProvider);
            return (TResponse)result;
        }
    }
}

using LoanMe.Infrastructure.Mediator.Abstractions;

namespace LoanMe.Infrastructure.Mediator {
    public class Mediator : IMediator {
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) {
            throw new NotImplementedException();
        }
    }
}

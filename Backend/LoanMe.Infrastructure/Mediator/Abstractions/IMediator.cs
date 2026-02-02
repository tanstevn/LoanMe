namespace LoanMe.Infrastructure.Mediator.Abstractions {
    public interface IMediator {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}

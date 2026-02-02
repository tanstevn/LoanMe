namespace LoanMe.Infrastructure.Mediator.Abstractions {
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}

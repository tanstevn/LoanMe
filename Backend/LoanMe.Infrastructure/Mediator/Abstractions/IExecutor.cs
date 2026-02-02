namespace LoanMe.Infrastructure.Mediator.Abstractions {
    public interface IExecutor {
        Task<object> ExecuteAsync(object request, IServiceProvider serviceProvider);
    }
}

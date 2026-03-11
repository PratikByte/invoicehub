using InvoiceAPI.Data.BaseRepo;
using MediatR;

public class AddCommand<T> : IRequest<T> where T : class
{
    public T Entity { get; }

    public AddCommand(T entity)
    {
        Entity = entity;
    }

}
public class AddCommandHandler<T> : IRequestHandler<AddCommand<T>, T> where T : class
{
    private readonly IBaseRepository<T> _repository;
    private readonly ILogger<AddCommandHandler<T>> _logger;

    public AddCommandHandler(IBaseRepository<T> repository, ILogger<AddCommandHandler<T>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<T> Handle(AddCommand<T> request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.AddAsync(request.Entity, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding entity of type {typeof(T).Name}");
            throw;
        }
        }
}

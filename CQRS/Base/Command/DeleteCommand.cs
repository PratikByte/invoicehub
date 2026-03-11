using InvoiceAPI.Data.BaseRepo;
using MediatR;

namespace InvoiceAPI.CQRS.Base.Command
{
    public class DeleteCommand<T> : IRequest<bool> where T : class
    {
        public int Id { get; }
        public DeleteCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, bool> where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;
        private readonly ILogger<DeleteCommandHandler<T>> _logger;

        public DeleteCommandHandler(IBaseRepository<T> baseRepository, ILogger<DeleteCommandHandler<T>> logger)
        {
            _baseRepository = baseRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)

        {
            try
            {
                return await _baseRepository.DeleteAsync(request.Id, cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting entity of type {typeof(T).Name} with Id {request.Id}");
                throw;
            }
        }
    }
}

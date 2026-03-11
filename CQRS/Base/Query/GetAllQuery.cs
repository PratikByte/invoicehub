using InvoiceAPI.Data.BaseRepo;
using MediatR;

namespace InvoiceAPI.CQRS.Base.Query
{
    public class GetAllQuery<T>:IRequest<IEnumerable<T>> where T: class
    {
        
    }

    public class GetAllQueryHandler<T>:IRequestHandler<GetAllQuery<T> ,IEnumerable<T>> where T : class
    {
        private readonly IBaseRepository<T> _repository;


        public GetAllQueryHandler(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> Handle(GetAllQuery<T> request, CancellationToken cancellationToken)
        {
           return await _repository.GetAllAsync(cancellationToken);
        }
    }
}

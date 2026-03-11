using InvoiceAPI.CQRS.Base.Command;
using InvoiceAPI.CQRS.Base.Query;
using InvoiceAPI.Data;
using InvoiceAPI.Entity;
using InvoiceAPI.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMediator _mediator;
        private readonly InvoiceDbContext _context;

        public CustomerController(ISupplierRepository supplierRepository, IMediator mediator, InvoiceDbContext context)
        {
            _supplierRepository = supplierRepository;
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer, CancellationToken token)
        {
            var result = await _mediator.Send(new AddCommand<Customer>(customer), token);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllQuery<Customer>(), cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCommand<Customer>(id), cancellationToken);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(int id, [FromBody] Customer entity)
        {
            var updatedEntity = await _mediator.Send(new UpdateCommand<Customer> { Id = id, Entity = entity });
            return Ok(updatedEntity);
        }
    }
}

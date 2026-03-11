using InvoiceAPI.Common.Pagination;
using InvoiceAPI.CQRS.Base.Command;
using InvoiceAPI.CQRS.Base.Query;
using InvoiceAPI.CQRS.Command;
using InvoiceAPI.CQRS.Query;
using InvoiceAPI.Data;
using InvoiceAPI.DTOs;
using InvoiceAPI.Entity;
using InvoiceAPI.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMediator _mediator;
        private readonly InvoiceDbContext _context;

        public SupplierController(ILogger<SupplierController> logger, ISupplierRepository supplierRepository, IMediator mediator, InvoiceDbContext context)
        {
            _logger = logger;
            _supplierRepository = supplierRepository;
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierCreateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var command = new CreateSupplierCommand(dto);
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supplier");
                return StatusCode(500, "An error occurred while creating the supplier.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers([FromQuery] PaginationParams paginationParams,CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAllSuppliersQuery(paginationParams);
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching suppliers");
                return StatusCode(500, "An error occurred while fetching the suppliers.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _supplierRepository.DeleteSupplierAsync(id, cancellationToken);
                if (!result)
                    return NotFound($"Supplier with id {id} not found.");
                return Ok($"Supplier with id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting supplier");
                return StatusCode(500, "An error occurred while deleting the supplier.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierUpdateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var command = new UpdateSupplierCommand(id, dto);
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating supplier");
                return StatusCode(500, "An error occurred while updating the supplier.");
            }
        }

        [HttpGet("by-Mobile/{MobileNo}")]
        public async Task<IActionResult> GetByMobile(string MobileNo, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByNumberAsync(MobileNo, cancellationToken);
            if (supplier == null)
            {
                return NotFound($"Supplier with Mobile {MobileNo} not found");

            }
            return Ok(supplier);
        }

        [HttpGet("search")]

        public async Task<ActionResult<IEnumerable<Supplier>>> searchSupplier([FromQuery] string name, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest("Name parameter is required.");
                var suppliers = await _context.Suppliers
                    .Where(s => s.Name.Contains(name))//partial search
                   .Take(5)// Limit to 5 results
                    .ToListAsync();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching suppliers");
                return StatusCode(500, "An error occurred while searching for suppliers.");
            }

        }

        

    }


}






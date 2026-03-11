using InvoiceAPI.Common.Pagination;
using InvoiceAPI.CQRS.Command;
using InvoiceAPI.CQRS.Query;
using InvoiceAPI.Dto;
using InvoiceAPI.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ItemController> _logger;
        private readonly IInvoiceRepository _itemRepository;

        public ItemController(IMediator mediator, ILogger<ItemController> logger, IInvoiceRepository itemRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _itemRepository = itemRepository;
        }        


        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemRequestDto dto,CancellationToken cancellationToken)
        {
            try
            {
                var command = new AddItemCommand(dto);
                var result = await _mediator.Send(command,cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating file");
                return StatusCode(500, "An error occurred while creating the item.");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> UpdateItem(int id, [FromBody] UpdateItemRequestDto dto,CancellationToken cancellationToken)
        {
            try
            {
               dto.ItemId = id;
                var command = new UpdateItemCommand(dto);
                var result = await _mediator.Send(command,cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating file");
                return StatusCode(500, "An error occurred while Updating the item.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ItemDto>>> GetAllItem([FromQuery]PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var query = new GetAllItemsQuery(paginationParams);
            var result = await _mediator.Send(query,cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteItemCommand(id);
                var result = await _mediator.Send(command,cancellationToken);

                if (!result)
                {
                    return NotFound($"Item with Id={id} not found.");
                }

                return Ok($"Item with Id={id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item Id={id}");
                return StatusCode(500, "An error occurred while deleting the item.");
            }
        }
    }
}


using InvoiceAPI.CQRS.Command;
using InvoiceAPI.Data;
using InvoiceAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IMediator mediator, ILogger<InvoicesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }



        //Create invoice
        [HttpPost]
        public ActionResult<InvoiceResponseDto> CreateInvoice([FromBody] InvoiceRequestDto request)
        {
            var command = new CreateInvoiceCommand(request);
            var result = _mediator.Send(new CreateInvoiceCommand(request)).Result;
            return Ok(result);
        }

        // //Get all invoices
        [HttpGet]
        public ActionResult<List<InvoiceResponseDto>> GetAllInvoices()
        {
            var query = new CQRS.Queries.GetInvoicesAll();
            var result = _mediator.Send(query).Result;
            return Ok(result);
        }
        
        
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoController : ControllerBase
    {

        [HttpPost]
        [Route("consultarSaldo")]
        public IActionResult Create([FromServices] IMediator mediator, [FromBody] SaldoRequest command)
        {
            var response = mediator.Send(command);
            if (response.Result.Falha is not null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

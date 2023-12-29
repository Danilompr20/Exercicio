using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;


namespace Questao5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {

        [HttpPost]
        [Route("criarMovimento")]
        public IActionResult Create([FromServices] IMediator mediator,[FromBody] MovimentacaoRequest command)
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

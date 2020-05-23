using System.Threading.Tasks;
using ForumBXS.Shared.Bus;
using Microsoft.AspNetCore.Mvc;
using Questions.Domain.Commands;
using Questions.Domain.Repositories;

namespace ForumBXS.WebAPI.Controllers
{
    [ApiController]
    [Route("v1/question")]
    public class QuestionController : ControllerBase
    {
        private readonly IMediatorHandler _bus;

        public QuestionController(IMediatorHandler bus)
        {
            _bus = bus;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] NewQuestionCommand command)
        {
            var result = await _bus.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromServices] IQuestionRepository repository)
        {
            var result = await repository.GetAll();
            return Ok(result);
        }
    }
}

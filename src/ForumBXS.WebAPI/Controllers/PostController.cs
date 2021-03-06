using System;
using System.Threading.Tasks;
using ForumBXS.Shared.Bus;
using Microsoft.AspNetCore.Mvc;
using Posts.Domain.Commands;
using Posts.Domain.Repositories;

namespace ForumBXS.WebAPI.Controllers
{
    [ApiController]
    [Route("v1/post")]
    public class PostController : ControllerBase
    {
        private readonly IMediatorHandler _bus;

        public PostController(IMediatorHandler bus)
        {
            _bus = bus;
        }

        [Route("question")]
        [HttpPost]
        public async Task<ActionResult> QuestionPost(
            [FromBody] NewQuestionCommand command)
        {
            var result = await _bus.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("question")]
        [HttpGet]
        public async Task<ActionResult> QuestionGetAll(
            [FromServices] IQuestionRepository repository)
        {
            var result = await repository.GetAll();
            return Ok(result);
        }

        [Route("question/{id}")]
        [HttpGet]
        public async Task<ActionResult> QuestionGetById(
            [FromRoute] Guid id,
            [FromServices] IQuestionRepository repository)
        {
            var result = await repository.GetById(id);
            return Ok(result);
        }

        [Route("answer")]
        [HttpPost]
        public async Task<ActionResult> AnswerPost(
            [FromBody] NewAnswerCommand command)
        {
            var result = await _bus.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("question/{id}/like")]
        [HttpPost]
        public async Task<ActionResult> QuestionLike(
            [FromRoute] Guid id)
        {
            var command = new LikeQuestionCommand { QuestionId = id };
            var result = await _bus.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("answer/{id}/like")]
        [HttpPost]
        public async Task<ActionResult> AnswerLike(
            [FromRoute] Guid id)
        {
            var command = new LikeAnswerCommand { AnswerId = id };
            var result = await _bus.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

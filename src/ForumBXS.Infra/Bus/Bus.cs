using System.Threading.Tasks;
using ForumBXS.Shared.Bus;
using ForumBXS.Shared.Commands;
using MediatR;

namespace ForumBXS.Infra.Bus
{
    public sealed class Bus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<CommandResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
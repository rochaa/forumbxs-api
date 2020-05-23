using ForumBXS.Shared.Commands;
using MediatR;

namespace ForumBXS.Shared.Handlers
{
    public interface IHandler<T> : IRequestHandler<T, CommandResult> where T : Command { }
}
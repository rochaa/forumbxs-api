using Flunt.Notifications;
using MediatR;

namespace ForumBXS.Shared.Commands
{
    public abstract class Command : Notifiable, IRequest<CommandResult>
    {
        public abstract void Validate();
    }
}
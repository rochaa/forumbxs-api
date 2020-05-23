using System.Collections.Generic;
using Flunt.Notifications;

namespace ForumBXS.Shared.Commands
{
    public class CommandResult
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommandResult(string message, IReadOnlyCollection<Notification> errors)
        {
            Sucess = false;
            Message = message;
            Data = errors.OnlyMessageErrors();
        }

        public CommandResult(string message, object dados)
        {
            Sucess = true;
            Message = message;
            Data = dados;
        }

        public CommandResult(string message)
        {
            Sucess = false;
            Message = message;
        }
    }
}
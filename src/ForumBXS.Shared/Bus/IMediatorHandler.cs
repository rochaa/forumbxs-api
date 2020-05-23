using System.Threading.Tasks;
using ForumBXS.Shared.Commands;

namespace ForumBXS.Shared.Bus
{
    public interface IMediatorHandler
    {
        Task<CommandResult> SendCommand<T>(T command) where T : Command;
    }
}

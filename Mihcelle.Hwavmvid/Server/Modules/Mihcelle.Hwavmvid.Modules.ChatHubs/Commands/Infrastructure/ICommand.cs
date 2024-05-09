using Oqtane.ChatHubs.Models;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    public interface ICommand
    {
        Task Execute(CommandServicesContext commandContext, CommandCallerContext callerContext, string[] args, ChatHubUser user);
    }
}
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server
{
    public interface IModuleinstallerinterface
    {

        Task Install();
        Task Deinstall();
        Task Removemodule(string id);
        Applicationmodulepackage applicationmodulepackage { get; }

    }
}

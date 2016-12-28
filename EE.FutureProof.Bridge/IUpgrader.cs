using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IUpgrader
    {
        int FromVersion { get; }
        int ToVersion { get; }
        Message UpgradeSend(object sender, Message m);
        Message DowngradeReceive(object sender, Message m);
    }
}
using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IUpgrader
    {
        int FromVersion { get; }
        int ToVersion { get; }
        Message UpgradeSend(Message m);
        Message DowngradeReceive(Message m);
    }
}
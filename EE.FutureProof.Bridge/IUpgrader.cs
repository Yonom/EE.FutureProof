using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IUpgrader
    {
        Message UpgradeSend(object sender, Message m);
        Message DowngradeReceive(object sender, Message m);
    }
}
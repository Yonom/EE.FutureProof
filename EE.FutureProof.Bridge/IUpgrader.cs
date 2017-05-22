using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IUpgrader
    {
        Message UpgradeSend(IFutureProofConnection sender, Message m);
        Message DowngradeReceive(IFutureProofConnection sender, Message m);
    }
}
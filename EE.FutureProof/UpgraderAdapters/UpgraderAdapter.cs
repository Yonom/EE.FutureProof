using PlayerIOClient;

namespace EE.FutureProof
{
    internal class UpgraderAdapter
    {
        public virtual Message UpgradeSend(Message m)
        {
            return m;
        }

        public virtual Message DowngradeReceive(Message m)
        {
            return m;
        }
    }
}
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class UpgraderAdapter
    {
        public virtual Message UpgradeSend(Message m)
        {
            return m;
        }

        public virtual Message UpgradeReceive(Message m)
        {
            return m;
        }
    }
}
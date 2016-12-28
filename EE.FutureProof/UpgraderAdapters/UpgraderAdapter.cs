using PlayerIOClient;

namespace EE.FutureProof
{
    internal class UpgraderAdapter
    {
        public virtual Message UpgradeSend(object sender, Message m)
        {
            return m;
        }

        public virtual Message DowngradeReceive(object sender, Message m)
        {
            return m;
        }
    }
}
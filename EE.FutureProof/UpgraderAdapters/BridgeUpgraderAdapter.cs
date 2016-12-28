using EE.FutureProof.Bridge;
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class BridgeUpgraderAdapter : UpgraderAdapter, IUpgrader
    {
        private readonly IUpgrader _upgrader;

        public BridgeUpgraderAdapter(IUpgrader upgrader)
        {
            this._upgrader = upgrader;
        }

        public override Message UpgradeSend(object sender, Message m)
        {
            return this._upgrader.UpgradeSend(sender, m);
        }

        public override Message DowngradeReceive(object sender, Message m)
        {
            return this._upgrader.DowngradeReceive(sender, m);
        }
    }
}
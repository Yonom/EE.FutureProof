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

        public int FromVersion => this._upgrader.FromVersion;
        public int ToVersion => this._upgrader.ToVersion;

        public override Message UpgradeSend(Message m)
        {
            return this._upgrader.UpgradeSend(m);
        }

        public override Message DowngradeReceive(Message m)
        {
            return this._upgrader.DowngradeReceive(m);
        }
    }
}
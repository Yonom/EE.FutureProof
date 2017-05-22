using System.Collections.Generic;
using System.Linq;
using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    internal class AggregateUpgrader : IUpgrader
    {
        public List<IUpgrader> Upgraders { get; } = new List<IUpgrader>();

        public Message UpgradeSend(IFutureProofConnection sender, Message m)
        {
            return this.Upgraders.Aggregate(m, (current, upgrader) => upgrader.UpgradeSend(sender, current));
        }

        public Message DowngradeReceive(IFutureProofConnection sender, Message m)
        {
            var result = m;
            for (var i = this.Upgraders.Count - 1; i >= 0; i--) // Fold right
                result = this.Upgraders[i].DowngradeReceive(sender, result);
            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using EE.FutureProof.Bridge;
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class AggregateUpgrader : IUpgrader
    {
        public List<IUpgrader> Upgraders { get; } = new List<IUpgrader>();

        public Message UpgradeSend(object sender, Message m)
        {
            return this.Upgraders.Aggregate(m, (current, upgrader) => upgrader.UpgradeSend(sender, current));
        }

        public Message DowngradeReceive(object sender, Message m)
        {
            var result = m;
            for (var i = this.Upgraders.Count - 1; i >= 0; i--) // Fold right
            {
                result = this.Upgraders[i].DowngradeReceive(sender, result);
            }
            return result;
        }
    }
}
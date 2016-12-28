﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EE.FutureProof.Bridge;
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class AggregateUpgrader : IUpgrader
    {
        public List<IUpgrader> Upgraders { get; } = new List<IUpgrader>();

        public int FromVersion => this.Upgraders[0].FromVersion;
        public int ToVersion => this.Upgraders[this.Upgraders.Count - 1].ToVersion;

        public Message UpgradeSend(Message m)
        {
            return this.Upgraders.Aggregate(m, (current, upgrader) => upgrader.UpgradeSend(current));
        }

        public Message DowngradeReceive(Message m)
        {
            Message result = m;
            for (var i = this.Upgraders.Count - 1; i >= 0; i--) // Fold right
            {
                result = this.Upgraders[i].DowngradeReceive(result);
            }
            return result;
        }
    }
}
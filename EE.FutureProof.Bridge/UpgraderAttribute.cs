using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EE.FutureProof.Bridge
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UpgraderAttribute : Attribute
    {
        public int FromVersion { get; private set; }
        public int ToVersion { get; private set; }

        public UpgraderAttribute(int fromVersion, int toVersion)
        {
            this.FromVersion = fromVersion;
            this.ToVersion = toVersion;
        }
    }
}

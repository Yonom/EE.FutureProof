using System;

namespace EE.FutureProof.Bridge
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UpgraderAttribute : Attribute
    {
        public UpgraderAttribute(int fromVersion, int toVersion)
        {
            this.FromVersion = fromVersion;
            this.ToVersion = toVersion;
        }

        public int FromVersion { get; }
        public int ToVersion { get; }
    }
}
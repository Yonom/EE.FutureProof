using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EE.FutureProof.Bridge;

namespace EE.FutureProof
{
    internal static class BridgeServices
    {
        static BridgeServices()
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(IUpgrader));
                foreach (var upgraderClass in assembly.GetExportedTypes().Where(t => !t.IsInterface && typeof(IUpgrader).IsAssignableFrom(t)))
                {
                    var upgrader = (IUpgrader)Activator.CreateInstance(upgraderClass);
                    List<IUpgrader> version;
                    if (!Upgraders.TryGetValue(upgrader.FromVersion, out version)) Upgraders.Add(upgrader.FromVersion, version = new List<IUpgrader>());
                    version.Add(upgrader);
                }
            }
            catch (ApplicationException ex)
            {
                Trace.TraceError($"FutureProof: Unable to load bridge upgraders. {ex.Message} {ex.InnerException?.Message}");
                throw;
            }
        }

        private static Dictionary<int, List<IUpgrader>> Upgraders { get; } = new Dictionary<int, List<IUpgrader>>();

        internal static UpgraderAdapter GetBridgeAdapter(int fromVersion, int toVersion)
        {
            var aggregator = new AggregateUpgrader();
            while (fromVersion < toVersion)
            {
                List<IUpgrader> version;
                if (Upgraders.TryGetValue(fromVersion, out version))
                {
                    var upgrader = version
                        .Where(u => u.ToVersion <= toVersion)
                        .OrderByDescending(u => u.ToVersion)
                        .First();
                    aggregator.Upgraders.Add(upgrader);

                    fromVersion = toVersion;
                }
                else
                {
                    fromVersion++;
                }
            }
            return new BridgeUpgraderAdapter(aggregator);
        }
    }
}
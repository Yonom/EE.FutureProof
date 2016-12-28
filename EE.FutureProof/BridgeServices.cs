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
        class UpgraderData
        {
            public UpgraderAttribute Attribute { get; set; }
            public Type Type { get; set; }
        }

        static BridgeServices()
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(IUpgrader));
                var upgraders = assembly.GetExportedTypes().Where(t => !t.IsAbstract && typeof(IUpgrader).IsAssignableFrom(t));
                foreach (var upgrader in upgraders)
                {
                    var attr = (UpgraderAttribute)upgrader.GetCustomAttributes(
                        typeof(UpgraderAttribute), true).FirstOrDefault();
                    if (attr != null)
                    {
                        List<UpgraderData> version;
                        if (!Upgraders.TryGetValue(attr.FromVersion, out version))
                            Upgraders.Add(attr.FromVersion, version = new List<UpgraderData>());
                        version.Add(new UpgraderData { Attribute = attr, Type = upgrader});
                    }
                }
            }
            catch (ApplicationException ex)
            {
                Trace.TraceError($"FutureProof: Unable to load bridge upgraders. {ex.Message} {ex.InnerException?.Message}");
                throw;
            }
        }

        private static Dictionary<int, List<UpgraderData>> Upgraders { get; } = new Dictionary<int, List<UpgraderData>>();

        internal static UpgraderAdapter GetBridgeAdapter(int fromVersion, int toVersion)
        {
            var aggregator = new AggregateUpgrader();
            while (fromVersion < toVersion)
            {
                List<UpgraderData> version;
                if (Upgraders.TryGetValue(fromVersion, out version))
                {
                    var data = version
                        .Where(d => d.Attribute.ToVersion <= toVersion)
                        .OrderByDescending(d => d.Attribute.ToVersion)
                        .First();

                    var upgrader = (IUpgrader)Activator.CreateInstance(data.Type);
                    aggregator.Upgraders.Add(upgrader);

                    fromVersion = data.Attribute.ToVersion;
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
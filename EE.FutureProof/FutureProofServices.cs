using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace EE.FutureProof
{
    public static class FutureProofServices
    {
        private const string BridgeName = "EE.FutureProof.Bridge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=14aec7c9c76f1d99";

        static FutureProofServices()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name == BridgeName)
            {
                try
                {
                    var bytes = new WebClient().DownloadData("https://github.com/Yonom/EE.FutureProof/raw/master/bin/EE.FutureProof.Bridge.dll");
                    return Assembly.Load(bytes);
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"FutureProof: Unable to download remote bridge assembly. {ex.Message} {ex.InnerException?.Message}");
                    throw;
                }
            }

            return null;
        }

        internal static UpgraderAdapter GetAdapter(int fromVersion, int toVersion)
        {
            try
            {
                return BridgeServices.GetBridgeAdapter(fromVersion, toVersion);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"FutureProof: Bridge is unavailable. {ex.Message} {ex.InnerException?.Message}");
                return new UpgraderAdapter();
            }
        }
    }
}
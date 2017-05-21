using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using EE.FutureProof.Bridge;

namespace EE.FutureProof
{
    internal static class FutureProofServices
    {
        private const string BridgeName = "EE.FutureProof.Bridge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=14aec7c9c76f1d99";
        private const string BridgeUrl = "https://github.com/Yonom/EE.FutureProof/raw/master/bin/EE.FutureProof.Bridge.dll";

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
                    var bytes = new WebClient().DownloadData(BridgeUrl);
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

        internal static FutureProofConnection GetConnection(IConnectionWrapper connection, int fromVersion, int toVersion)
        {
            try
            {
                return GetBridgeConnection(connection, fromVersion, toVersion);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"FutureProof: Bridge is unavailable. {ex.Message} {ex.InnerException?.Message}");
                return new FutureProofConnection(connection);
            }
        }

        private static FutureProofConnection GetBridgeConnection(IConnectionWrapper connection, int fromVersion, int toVersion)
        {
            return new BridgeFutureProofConnection(connection, UpgradeServices.GetUpgrader(fromVersion, toVersion));
        }
    }
}
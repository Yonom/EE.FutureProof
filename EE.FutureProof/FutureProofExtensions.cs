using PlayerIOClient;

namespace EE.FutureProof
{
    public static class FutureProofExtensions
    {
        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion)
        {
            var toVersion = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest", null).BigDB.Load("Config", "config").GetInt("version");
            return connection.FutureProof(fromVersion, toVersion);
        }

        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion, int toVersion)
        {
            return new FutureProofConnection(connection, FutureProofServices.GetAdapter(fromVersion, toVersion));
        }
    }
}
using PlayerIOClient;

namespace EE.FutureProof
{
    public static class FutureProofExtensions
    {
        private const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion)
        {
            var toVersion = PlayerIO.QuickConnect.SimpleConnect(GameId, "guest", "guest", null).BigDB.Load("Config", "config").GetInt("version");
            return connection.FutureProof(fromVersion, toVersion);
        }

        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion, int toVersion)
        {
            return new FutureProofConnection(connection, FutureProofServices.GetAdapter(fromVersion, toVersion));
        }
    }
}
﻿using PlayerIOClient;

namespace EE.FutureProof
{
    public static class FutureProofExtensions
    {
        private const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";
        
        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion)
        {
            return new PlayerIOConnection(connection).FutureProof(fromVersion);
        }

        public static FutureProofConnection FutureProof(this Connection connection, int fromVersion, int toVersion)
        {
            return new PlayerIOConnection(connection).FutureProof(fromVersion, toVersion);
        }
        
        public static FutureProofConnection FutureProof(this IConnectionWrapper connection, int fromVersion)
        {
            var toVersion = PlayerIO.QuickConnect.SimpleConnect(GameId, "guest", "guest", null).BigDB.Load("Config", "config").GetInt("version");
            return connection.FutureProof(fromVersion, toVersion);
        }

        public static FutureProofConnection FutureProof(this IConnectionWrapper connection, int fromVersion, int toVersion)
        {
            return FutureProofServices.GetConnection(connection, fromVersion, toVersion);
        }
    }
}
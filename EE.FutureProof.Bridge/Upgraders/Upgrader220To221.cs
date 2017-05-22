using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    [Upgrader(220, 221)]
    internal class Upgrader220To221 : IUpgrader
    {
        public Message UpgradeSend(IFutureProofConnection sender, Message m)
        {
            switch (m.Type)
            {
                case "setRoomVisible":
                    return m.ToEnumerable()
                        .Add(false)
                        .ToMessage();

                default:
                    return m;
            }
        }

        public Message DowngradeReceive(IFutureProofConnection sender, Message m)
        {
            switch (m.Type)
            {
                case "add":
                     return m.ToEnumerable()
                        .RemoveAt(23)
                        .ToMessage();

                default:
                    return m;
            }
        }
    }
}
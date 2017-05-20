using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    [Upgrader(220, 221)]
    public class Upgrader220To221 : IUpgrader
    {
        public Message UpgradeSend(object sender, Message m)
        {
            return m;
        }

        public Message DowngradeReceive(object sender, Message m)
        {
            switch (m.Type)
            {
                case "add":
                    return m.ToEnumerable()
                        .RemoveAt(22)
                        .RemoveAt(23)
                        .RemoveAt(24)
                        .ToMessage();

                default:
                    return m;
            }
        }
    }
}
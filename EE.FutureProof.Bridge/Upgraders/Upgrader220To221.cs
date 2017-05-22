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
                case "temp_Upgrader220To221_add":
                    return m.ToEnumerable()
                        .SetType("add")
                        .ToMessage();

                case "add":
                    sender.Receive(m.ToEnumerable() 
                        .SetType("temp_Upgrader220To221_add")
                        .RemoveAt(22)
                        .RemoveAt(23)
                        .ToMessage());
                     return Message.Create("psi", m[0], m[22]);

                default:
                    return m;
            }
        }
    }
}
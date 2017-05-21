using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    [Upgrader(206, 207)]
    public class Upgrader206To207 : IUpgrader
    {
        private const string Token = "..";

        public Message UpgradeSend(object sender, Message m)
        {
            switch (m.Type)
            {
                case Token:
                    return m.ToEnumerable()
                        .SetType("b")
                        .ToMessage();

                case Token + "k":
                    return m.ToEnumerable()
                        .SetType("crown")
                        .ToMessage();

                case Token + "f":
                    return m.ToEnumerable()
                        .SetType("smiley")
                        .ToMessage();

                case Token + "r":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("red")
                        .ToMessage();

                case Token + "g":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("green")
                        .ToMessage();

                case Token + "b":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("blue")
                        .ToMessage();

                case Token + "y":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("yellow")
                        .ToMessage();

                case Token + "m":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("magenta")
                        .ToMessage();

                case Token + "c":
                    return m.ToEnumerable()
                        .SetType("pressKey")
                        .Add("cyan")
                        .ToMessage();

                default:
                    return m;
            }
        }

        public Message DowngradeReceive(object sender, Message m)
        {
            switch (m.Type)
            {
                case "init":
                    return m.ToEnumerable()
                        .InsertAt(5, Token)
                        .ToMessage();

                default:
                    return m;
            }
        }
    }
}
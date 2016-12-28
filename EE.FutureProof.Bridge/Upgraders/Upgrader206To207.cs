using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    [Upgrader(206, 207)]
    public class Upgrader206To207 : IUpgrader
    {
        private const string Token = "..";
        public Message UpgradeSend(object sender, Message m)
        {
            if (m.Type == Token)
            {
                var message = Message.Create("b");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                return message;
            }

            if (m.Type == Token + "k")
            {
                var message = Message.Create("crown");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                return message;
            }

            if (m.Type == Token + "f")
            {
                return Message.Create("smiley", m[0]);
            }

            if (m.Type == Token + "r")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("red");

                return message;
            }

            if (m.Type == Token + "g")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("green");

                return message;
            }

            if (m.Type == Token + "b")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("blue");

                return message;
            }

            if (m.Type == Token + "y")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("yellow");

                return message;
            }

            if (m.Type == Token + "m")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("magenta");

                return message;
            }

            if (m.Type == Token + "c")
            {
                var message = Message.Create("pressKey");
                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                message.Add("cyan");

                return message;
            }

            return m;
        }

        public Message DowngradeReceive(object sender, Message m)
        {
            if (m.Type == "init")
            {
                var message = Message.Create("init",
                        m[0],
                        m[1],
                        m[2],
                        m[3],
                        m[4],
                        Token);

                for (uint i = 0; i < m.Count; i++)
                {
                    message.Add(m[i]);
                }

                return message;
            }

            return m;
        }
    }
}
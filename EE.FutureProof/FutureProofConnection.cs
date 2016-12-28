using PlayerIOClient;

namespace EE.FutureProof
{
    public class FutureProofConnection
    {
        private readonly UpgraderAdapter _adapter;

        internal FutureProofConnection(Connection connection, UpgraderAdapter adapter)
        {
            this._adapter = adapter;
            this.InternalConnection = connection;
            this.InternalConnection.OnMessage += this.InternalConnection_OnMessage;
        }

        public Connection InternalConnection { get; set; }

        public bool Connected => this.InternalConnection.Connected;

        private void InternalConnection_OnMessage(object sender, Message m)
        {
            this.OnMessage?.Invoke(sender, this._adapter.DowngradeReceive(m));
        }

        public void Send(Message message)
        {
            this.InternalConnection.Send(this._adapter.UpgradeSend(message));
        }

        public void Send(string type, params object[] parameters)
        {
            this.Send(Message.Create(type, parameters));
        }

        public void Disconnect()
        {
            this.InternalConnection.Disconnect();
        }

        public event MessageReceivedEventHandler OnMessage;

        public event DisconnectEventHandler OnDisconnect
        {
            add { this.InternalConnection.OnDisconnect += value; }
            remove { this.InternalConnection.OnDisconnect -= value; }
        }

        public void AddOnMessage(MessageReceivedEventHandler handler)
        {
            this.OnMessage += handler;
        }

        public void AddOnDisconnect(DisconnectEventHandler handler)
        {
            this.OnDisconnect += handler;
        }

        public void Dispose()
        {
            this.InternalConnection.Disconnect();
        }
    }
}
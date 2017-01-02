using PlayerIOClient;

namespace EE.FutureProof
{
    public class FutureProofConnection : IConnectionWrapper
    {
        private readonly UpgraderAdapter _adapter;
        public readonly IConnectionWrapper _innerConnection;

        internal FutureProofConnection(IConnectionWrapper connection, UpgraderAdapter adapter)
        {
            this._adapter = adapter;
            this._innerConnection = connection;
            this._innerConnection.OnMessage += this.InternalConnection_OnMessage;
        }

        public Connection InternalConnection => this._innerConnection.InternalConnection;
        public bool Connected => this._innerConnection.Connected;

        private void InternalConnection_OnMessage(object sender, Message m)
        {
            this.OnMessage?.Invoke(sender, this._adapter.DowngradeReceive(this, m));
        }

        public void Send(Message m)
        {
            this._innerConnection.Send(this._adapter.UpgradeSend(this, m));
        }

        public void Send(string type, params object[] parameters)
        {
            this.Send(Message.Create(type, parameters));
        }

        public void Disconnect()
        {
            this._innerConnection.Disconnect();
        }

        public event MessageReceivedEventHandler OnMessage;

        public event DisconnectEventHandler OnDisconnect
        {
            add { this._innerConnection.OnDisconnect += value; }
            remove { this._innerConnection.OnDisconnect -= value; }
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
            this._innerConnection.Disconnect();
        }
    }
}
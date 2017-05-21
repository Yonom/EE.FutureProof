using EE.FutureProof.Bridge;
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class BridgeFutureProofConnection : FutureProofConnection, IFutureProofConnection
    {
        private readonly IUpgrader _adapter;

        public BridgeFutureProofConnection(IConnectionWrapper connection, IUpgrader adapter) 
            : base(connection)
        {
            this._adapter = adapter;
        }

        protected override void Receive(Message m)
        {
            this.ReceiveDirect(this._adapter.DowngradeReceive(this, m));
        }

        public override void Send(Message m)
        {
            this.SendDirect(this._adapter.UpgradeSend(this, m));
        }

        public void SendDirect(Message m)
        {
            base.Send(m);
        }

        public void ReceiveDirect(Message m)
        {
            base.Receive(m);
        }
    }
}
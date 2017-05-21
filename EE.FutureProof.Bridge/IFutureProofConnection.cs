using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IFutureProofConnection
    {
        void SendDirect(Message m);
        void ReceiveDirect(Message m);
    }
}
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UniRx;

// Reference: http://qiita.com/Napier_271828_/items/0d489c7d833047241079
public class UdpState : System.IEquatable<UdpState> {
    //UDP通信の情報を収める。送受信ともに使える
    public IPEndPoint EndPoint {
        get; set;
    }
    public string UdpMsg {
        get; set;
    }

    public UdpState(IPEndPoint ep, string udpMsg) {
        this.EndPoint = ep;
        this.UdpMsg = udpMsg;
    }
    public override int GetHashCode() {
        return EndPoint.Address.GetHashCode();
    }

    public bool Equals(UdpState s) {
        if (s == null) {
            return false;
        }
        return EndPoint.Address.Equals(s.EndPoint.Address);
    }
}

public class UDPReceiver : MonoBehaviour {
    private const int listenPort = 2222;
    private static UdpClient udpClient;
    private bool isReceiving = false;
    private bool isApplicationQuit = false;
    public IObservable<UdpState> _udpSequence;

    void Awake() {
        _udpSequence = Observable.Create<UdpState>(observer => {
            Debug.Log(string.Format("_udpSequence thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            try {
                udpClient = new UdpClient(listenPort);
            }
            catch (SocketException ex) {
                observer.OnError(ex);
            }
            IPEndPoint remoteEP = null;
            udpClient.EnableBroadcast = true;
            while (!isApplicationQuit) {
                try {
                    remoteEP = null;
                    var receivedMsg = System.Text.Encoding.ASCII.GetString(udpClient.Receive(ref remoteEP));
                    observer.OnNext(new UdpState(remoteEP, receivedMsg));
                }
                catch (SocketException) {
                    Debug.Log("UDP::Receive timeout");
                }
            }
            observer.OnCompleted();
            return Disposable.Empty;
        })
        .Where(_ => isReceiving)
        .SubscribeOn(Scheduler.ThreadPool)
        .Publish()
        .RefCount();
    }

    public void StartSubscribe() {
        isReceiving = true;
    }

    public void StopSubscribe() {
        isReceiving = false;
    }

    void OnApplicationQuit() {
        isApplicationQuit = true;
        udpClient.Client.Blocking = false;
    }
}
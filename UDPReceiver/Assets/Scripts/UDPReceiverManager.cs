using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UDPReceiverManager : MonoBehaviour {

    public UDPReceiver udpReceiver;
    IObservable<UdpState> udpSequence;
    public Text ReceiveText;

	void Start () {
        udpSequence = udpReceiver._udpSequence;
        udpSequence.ObserveOnMainThread()
                   .Subscribe(x => {
                       Debug.Log(x.UdpMsg);
                       ReceiveText.text = x.UdpMsg;
                   })
                   .AddTo(this);
	}

    public void StartSubscribeButtonClick() {
        udpReceiver.StartSubscribe();
    }

    public void StopSubscribeButtonClick() {
        udpReceiver.StopSubscribe();
    }

}

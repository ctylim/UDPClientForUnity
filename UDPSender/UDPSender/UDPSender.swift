//
//  UDPSender.swift
//  UDPSender
//
//  Created by ctylim on 2017/05/05.
//  Copyright © 2017年 ctylim. All rights reserved.
//

import Foundation
import CocoaAsyncSocket

class UDPSender: NSObject, GCDAsyncUdpSocketDelegate {
    var socket: GCDAsyncUdpSocket!
    
    override init(){
        super.init()
        setupConnection()
    }
    
    func setupConnection(){
        socket = GCDAsyncUdpSocket(delegate: self, delegateQueue: DispatchQueue.main)
    }
    
    func send(message:String){
        let data = message.data(using: String.Encoding.utf8)
        socket.send(data!, toHost: "localhost", port: 2222, withTimeout: 2, tag: 0)
    }
    
    func udpSocket(_ sock: GCDAsyncUdpSocket, didNotConnect error: Error?) {
        print("didNotConnect")
    }
    
    func udpSocket(_ sock: GCDAsyncUdpSocket, didSendDataWithTag tag: Int) {
        print("didSendDataWithTag")
    }
    
    func udpSocket(_ sock: GCDAsyncUdpSocket, didConnectToAddress address: Data) {
        print("didConnectToAddress")
    }
}

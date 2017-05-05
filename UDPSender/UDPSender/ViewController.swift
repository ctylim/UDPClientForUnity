//
//  ViewController.swift
//  UDPSender
//
//  Created by ctylim on 2017/05/05.
//  Copyright © 2017年 ctylim. All rights reserved.
//

import Cocoa

class ViewController: NSViewController {
    
    var udpSender: UDPSender!

    override func viewDidLoad() {
        super.viewDidLoad()
        udpSender = UDPSender()
        // Do any additional setup after loading the view.
    }

    override var representedObject: Any? {
        didSet {
        // Update the view, if already loaded.
        }
    }

    @IBAction func sendButtonClicked(_ sender: Any) {
        startStreaming()
    }
    
    var timer: DispatchSourceTimer?
    var count: Int32 = 0
    
    func startStreaming() {
        let queue = DispatchQueue(label: "timer")
        timer = DispatchSource.makeTimerSource(queue: queue)
        timer?.scheduleRepeating(deadline: .now(), interval: .milliseconds(1000))
        timer?.setEventHandler(handler: {
            self.sendMessage()
        })
        timer?.resume()
    }
    
    func sendMessage() {
        print("send " + String(count))
        udpSender.send(message: "send " + String(count))
        count += 1
    }
    
    func stopStreaming() {
        timer?.cancel()
        timer = nil
    }

}


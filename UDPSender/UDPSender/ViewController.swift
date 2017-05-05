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
        udpSender.send(message: "send message\n")
    }

}


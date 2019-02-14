﻿#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;

public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;
    private IDictionary<string, string> json_data = new Dictionary<string, string>();

    public float alpha;
    public float beta;
    public float gamma;

	public void Start() 
	{
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("open", connectionOpen);
		socket.On("unityMsg", unityMsgHandler);
		socket.On("error", connectionError);
		socket.On("close", connectionClose);
	}

    private void Update()
    {
        socket.Emit("angleRequestMsg");
    }

    public void connectionOpen(SocketIOEvent e)
	{

        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
	
	public void unityMsgHandler(SocketIOEvent e)
	{
		// Debug.Log("[SocketIO] Angles received: " + e.name + " " + e.data);

        json_data = e.data.ToDictionary();

        alpha = float.Parse(json_data["alpha"]);
        beta = float.Parse(json_data["beta"]);
        gamma = float.Parse(json_data["gamma"]);
    }
	
	public void connectionError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void connectionClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
}

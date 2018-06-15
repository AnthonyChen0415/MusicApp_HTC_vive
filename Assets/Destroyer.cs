using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class Destroyer : MonoBehaviour {
    public GameObject obj;
    // Use this for initialization
        Socket socket;
        byte[] buffer = new byte[1024];

        void Start()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            socket.Blocking = false;

            StartCoroutine(Poll());
        }

        IEnumerator Poll()
        {
            while (true)
            {
                yield return null;
                if (socket.Poll(0, SelectMode.SelectRead))
                {
                    int bytesReceived = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    if (bytesReceived > 0)
                    {
                        // process data
                    }
                }
            }
        
    }
}

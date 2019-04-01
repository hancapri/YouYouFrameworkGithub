using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestSocket : MonoBehaviour {
    private SocketTcpRoutine sc;
    // Use this for initialization
    void Start () {
        sc = new SocketTcpRoutine();
	}
	
	// Update is called once per frame
	void Update () {
        //sc.OnUpdate();
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameEntry.Socket.ConnectToMainSocket("192.168.0.106", 1010);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 200; i++)
            {
                System_SendLocalTimeProto proto = new System_SendLocalTimeProto();

                GameEntry.Socket.SendMsg(proto);
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DateTime t = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                byte[] buffer = null;
                using (MMO_MemoryStream ms = new MMO_MemoryStream())
                {
                    ms.WriteUShort(10);
                    ms.WriteFloat(100);
                    ms.WriteLong(100);
                    buffer = ms.ToArray();
                }

                using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                {
                    ushort u = ms.ReadUShort();
                    float f =  ms.ReadFloat();
                    long l = ms.ReadLong();
                }
            }
            TimeSpan ts = (DateTime.Now - t);
            Debug.Log("A耗时："+ts.TotalMilliseconds);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            DateTime t = DateTime.Now;
            MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
            for (int i = 0; i < 100000; i++)
            {
                byte[] buffer = null;
                ms.SetLength(0);
                ms.WriteUShort(10);
                ms.WriteFloat(100);
                ms.WriteLong(100);
                buffer = ms.ToArray();


                ms.SetLength(0);
                ms.Write(buffer,0,buffer.Length);
                ms.Position = 0;
                ushort u = ms.ReadUShort();
                float f = ms.ReadFloat();
                long l = ms.ReadLong();
            }
            TimeSpan ts = (DateTime.Now - t);
            Debug.Log("B耗时：" + ts.TotalMilliseconds);
        }
    }
}

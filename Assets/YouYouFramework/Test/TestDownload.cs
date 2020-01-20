using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestDownload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //GameEntry.Download.BeginDownload("download/ssss.rar");
            LinkedList<string> lstUrl = new LinkedList<string>();
            lstUrl.AddLast("download/cusshaders.assetbundle");
            lstUrl.AddLast("download/xlualogic.assetbundle");
            lstUrl.AddLast("download/ui/uifont.assetbundle");
            lstUrl.AddLast("download/ui/uiprefab.assetbundle");
            lstUrl.AddLast("download/scenes/gamescene_dali.assetbundle");
            GameEntry.Download.BeginDownloadMulit(lstUrl,onupdate,oncomplete);
        }
	}

    private void oncomplete()
    {
        Debug.Log("下载完毕");
    }

    private void onupdate(int t1, int t2, ulong t3, ulong t4)
    {
        Debug.Log(string.Format("下载中 当前数量{0}/{1}  当前大小（字节）{2}/{3}",t1,t2,t3,t4));
    }
}

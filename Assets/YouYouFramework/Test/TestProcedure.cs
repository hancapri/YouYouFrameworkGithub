using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestProcedure : MonoBehaviour {

    void Start()
    {
        VarInt a = VarInt.Alloc(10);

        StartCoroutine(ReleaseVar(a));
    }

    private IEnumerator ReleaseVar(VarInt a)
    {
        yield return new WaitForSeconds(5);
        a.Release();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    GameEntry.Log(LogCategory.Procedure, "当前的流程 = " + GameEntry.Procedure.CurrProcedure);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    GameEntry.DataTable.LoadDataTableAsync();
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.LogError("卸载分类资源池中所有资源");
            GameEntry.Pool.PoolManager.ReleaseAssetPool();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.LogError("强制卸载所有资源包");
            GameEntry.Pool.PoolManager.AssetBundlePool.ReleaseAll();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogError("卸载不用资源");
            Resources.UnloadUnusedAssets();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameEntry.UI.OpenUIForm(UIFormId.UI_Task);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameEntry.UI.CloseUIForm(UIFormId.UI_Task);
        }
    }

}

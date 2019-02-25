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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("当前的流程 = " + GameEntry.Procedure.CurrProcedure);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEntry.Procedure.ParamDic["name"] = "CheckVersion";
            GameEntry.Procedure.ParamDic["num"] = 12;
            GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.EnterGame);
        }
    }

}

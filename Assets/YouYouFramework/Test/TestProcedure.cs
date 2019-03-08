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
        string[] arr = new string[2] { "---", "+++" };
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEntry.Procedure.SetData("name", arr);
            GameEntry.Procedure.SetData("age", 20);

            GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
            GameEntry.Procedure.SetData("age", 25);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.EnterGame);
        }
    }

}

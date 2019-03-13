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

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.Preload);
        }
    }

}

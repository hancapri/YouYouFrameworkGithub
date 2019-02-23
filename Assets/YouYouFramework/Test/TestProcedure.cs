using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestProcedure : MonoBehaviour {

    void Start()
    {

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
            GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.EnterGame);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Sys_SceneDetailDBModel {
    private List<Sys_SceneDetailEntity> m_retLst = new List<Sys_SceneDetailEntity>(10);

    /// <summary>
    /// 根据场景编号获取场景明细
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public List<Sys_SceneDetailEntity> GetListBySceneId(int sceneId,int sceneGrade)
    {
        m_retLst.Clear();
        List<Sys_SceneDetailEntity> lst = this.GetList();
        int len = lst.Count;
        for (int i = 0; i < len; i++)
        {
            Sys_SceneDetailEntity entity = lst[i];
            if (entity.SceneGrade <= sceneGrade)
            {
                m_retLst.Add(entity);
            }
        }
        return m_retLst;
    }
}

//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-06-10 20:36:23
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UISelectRoleDragView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //开始拖拽的位置
    private Vector2 m_DragBeginPos = Vector2.zero;

    //结束拖拽的位置
    private Vector2 m_DragEndPos = Vector2.zero;

    /// <summary>
    /// 拖拽委托 0=左 1=右
    /// </summary>
    public Action<int> OnDragComplete;

    void Start () 
	{
	
	}

    void OnDestroy()
    {
        OnDragComplete = null;
    }

    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragBeginPos = eventData.position;
    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        m_DragEndPos = eventData.position;

        float x = m_DragBeginPos.x - m_DragEndPos.x;

        //这个20是容错范围
        if (x > 20)
        {
            OnDragComplete(0);
        }
        else if (x < -20)
        {
            OnDragComplete(1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// UI对象池
    /// </summary>
    public class UIPool
    {
        private LinkedList<UIFormBase> m_UIFormList;

        public UIPool()
        {
            m_UIFormList = new LinkedList<UIFormBase>();
        }

        /// <summary>
        /// UI出池
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <returns></returns>
        internal UIFormBase Dequeue(int uiFormId)
        {
            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr !=null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiFormId)
                {
                    m_UIFormList.Remove(curr.Value);
                    return curr.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// UI回池
        /// </summary>
        /// <param name="form"></param>
        internal void Enqueue(UIFormBase form)
        {
            form.gameObject.SetActive(false);
            m_UIFormList.AddLast(form);
        }

        /// <summary>
        /// 检查是否可以释放过期UI
        /// </summary>
        internal void CheckClear()
        {
            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {
                if (!curr.Value.IsLook && Time.time > curr.Value.CloseTime + GameEntry.UI.UIExpire)
                {
                    //销毁UI
                    Object.Destroy(curr.Value.gameObject);
                    LinkedListNode<UIFormBase> next = curr.Next;
                    m_UIFormList.Remove(curr.Value);
                    curr = next;
                }
                else
                {
                    curr = curr.Next;
                }
            }
        }

        /// <summary>
        /// 检查是否可以释放过量的UI
        /// </summary>
        internal void CheckByOpenUI()
        {
            if (m_UIFormList.Count <= GameEntry.UI.UIPoolMaxCount) return;

            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {
                if (m_UIFormList.Count == GameEntry.UI.UIPoolMaxCount)
                {
                    //如果池中数量在指定范围内，则不再继续销毁
                    break;
                }
                if (!curr.Value.IsLook)
                {
                    //销毁UI
                    Object.Destroy(curr.Value.gameObject);
                    LinkedListNode<UIFormBase> next = curr.Next;
                    m_UIFormList.Remove(curr.Value);
                    curr = next;
                }
                else
                {
                    curr = curr.Next;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter
{
    //����
    private static EventCenter instance;
    public static EventCenter Instance => instance ??= new EventCenter();
    
    //key���¼����ƣ�value���¼�����Ӧ����
    private Dictionary<string,UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();

    /// <summary>
    /// ����¼�����
    /// </summary>
    /// <param name="eventName">�¼�������</param>
    /// <param name="action">�����¼��ĺ���</param>
    public void AddEventListener(string eventName, UnityAction<object> action)
    {
        //����¼��Ѿ����ڣ�����ӵ��¼��ֵ���
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName] += action;
        }
        //����¼������ڣ�����ӵ��¼��ֵ���
        else 
        {
            eventDic.Add(eventName, action);
        }
        
    }

    /// <summary>
    /// �¼�����
    /// </summary>
    /// <param name="eventName">�ĸ��¼�����</param>
    public void EventTrigger(string eventName,object data=null)
    {
        //����¼����ڣ��������Ӧ����
        eventDic[eventName]?.Invoke(data);
        //��ͬ��:
        //if (eventDic.ContainsKey(eventName))
        //{
        //    eventDic[eventName].Invoke();
        //}

    }



}

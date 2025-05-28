using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter
{
    //单例
    private static EventCenter instance;
    public static EventCenter Instance => instance ??= new EventCenter();
    
    //key是事件名称，value是事件的响应函数
    private Dictionary<string,UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件的名字</param>
    /// <param name="action">处理事件的函数</param>
    public void AddEventListener(string eventName, UnityAction<object> action)
    {
        //如果事件已经存在，则添加到事件字典中
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName] += action;
        }
        //如果事件不存在，则添加到事件字典中
        else 
        {
            eventDic.Add(eventName, action);
        }
        
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="eventName">哪个事件触发</param>
    public void EventTrigger(string eventName,object data=null)
    {
        //如果事件存在，则调用响应函数
        eventDic[eventName]?.Invoke(data);
        //等同于:
        //if (eventDic.ContainsKey(eventName))
        //{
        //    eventDic[eventName].Invoke();
        //}

    }



}

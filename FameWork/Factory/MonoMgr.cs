using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 公共Mono模块，用于管理各个Mono组件
/// </summary>
public class MonoMgr : MonoBehaviour
{
    private static MonoMgr instance;

    public static MonoMgr Instance
    {
        get
        {
            //如果MonoMgr还没有实例化，则创建一个GameObject，并添加MonoMgr组件
            if (instance == null)
            {
                GameObject go = new GameObject("MonoMgr");
                instance = go.AddComponent<MonoMgr>();
            }
            return instance;
        }
    }

    public event UnityAction updateEvent;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update() 
    {
        updateEvent?.Invoke();
    }

    /// <summary>
    /// 添加一个Update函数
    /// </summary>
    /// <param name="action">你要添加的函数</param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// 移除一个Update函数
    /// </summary>
    /// <param name="action">你要移除的函数</param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }

    public Coroutine _StartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
    public Coroutine _StartCoroutine(string methodName, object value)
    {
        return StartCoroutine(methodName, value);
    }
    public void _StopCoroutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
    public void _StopCoroutine(string methodName)
    {
        StopCoroutine(methodName);
    }


}

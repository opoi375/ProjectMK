using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����Monoģ�飬���ڹ������Mono���
/// </summary>
public class MonoMgr : MonoBehaviour
{
    private static MonoMgr instance;

    public static MonoMgr Instance
    {
        get
        {
            //���MonoMgr��û��ʵ�������򴴽�һ��GameObject�������MonoMgr���
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
    /// ���һ��Update����
    /// </summary>
    /// <param name="action">��Ҫ��ӵĺ���</param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// �Ƴ�һ��Update����
    /// </summary>
    /// <param name="action">��Ҫ�Ƴ��ĺ���</param>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr
{
    //单例模式
   private static PoolMgr instance;
   public static PoolMgr Instance => instance ??= new PoolMgr();

   public Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

   private GameObject poolParent;
    /// <summary>
    /// 从池中获取对象
    /// </summary>
    /// <returns>对象</returns>
    public GameObject GetObject(string name)
    {
        GameObject obj = null;
        //有抽屉，里面有东西
        if(poolDic.ContainsKey(name) && poolDic[name].Count > 0) 
        {
            obj = poolDic[name].Dequeue();
        }
        else 
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Fabs/"+name));
        }

        //激活
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;
        return obj;
    }

    public void PushObject(string name,GameObject obj,float time)
    {
        MonoMgr.Instance.StartCoroutine(DelayPush(name,obj,time));
    }

    private IEnumerator DelayPush(string name,GameObject obj,float time)
    {
        yield return new WaitForSeconds(time);
        PushObject(name,obj);
    }

    public void PushObject(string name,GameObject obj) 
    {
        if (poolParent == null)
        {
            poolParent = new GameObject("Pool");
        }
        //把obj放到poolParent下
        obj.transform.parent = poolParent.transform;


        //把不用的对象放回池子
        if (poolDic.ContainsKey(name)&&poolDic[name].Count >= 0)
        {
            //Debug.Log("有抽屉，放进去");
          //有抽屉，放进去
            poolDic[name].Enqueue(obj);
        }
        else 
        {
            //Debug.Log("没有抽屉，新建一个");
            //没有抽屉，新建一个
            poolDic.Add(name, new Queue<GameObject>());
            //放进去
            poolDic[name].Enqueue(obj);
        }
        //把obj失活
        obj.SetActive(false);

    }

    /// <summary>
    /// 清空池子的方法，用于场景切换的时候
    /// </summary>
     public void Clear() 
     {
        //清空池子，避免内存泄漏
         poolDic.Clear();
         poolParent = null;
     }

}

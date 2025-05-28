using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr
{
   private static ResourcesMgr instance;
   public static ResourcesMgr Instance => instance ??= new ResourcesMgr();

    //同步加载资源
    public T Load<T>(string path) where T:Object
    {
        T res = Resources.Load<T>(path);
        if (res is GameObject) 
        {
            //如果是GameObject，则实例化后返回。
           return GameObject.Instantiate(res);
        }
        else//TextAsset等资源直接返回。
        {
            return res;
        }
        
    }

    //异步加载资源
    public void LoadAsync<T>(string path,UnityAction<T> callback) where T : Object
    {
        //开启协程
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync<T>(path, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callback) where T : Object
    {
       ResourceRequest r = Resources.LoadAsync<T>(path);
       yield return r;

        if (r.asset is GameObject)
        {
            //如果是GameObject，则实例化后返回。
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else//TextAsset等资源直接返回。
        {
            callback(r.asset as T);
        }

    }

}

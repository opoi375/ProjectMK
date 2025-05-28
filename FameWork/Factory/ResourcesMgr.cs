using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr
{
   private static ResourcesMgr instance;
   public static ResourcesMgr Instance => instance ??= new ResourcesMgr();

    //ͬ��������Դ
    public T Load<T>(string path) where T:Object
    {
        T res = Resources.Load<T>(path);
        if (res is GameObject) 
        {
            //�����GameObject����ʵ�����󷵻ء�
           return GameObject.Instantiate(res);
        }
        else//TextAsset����Դֱ�ӷ��ء�
        {
            return res;
        }
        
    }

    //�첽������Դ
    public void LoadAsync<T>(string path,UnityAction<T> callback) where T : Object
    {
        //����Э��
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync<T>(path, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callback) where T : Object
    {
       ResourceRequest r = Resources.LoadAsync<T>(path);
       yield return r;

        if (r.asset is GameObject)
        {
            //�����GameObject����ʵ�����󷵻ء�
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else//TextAsset����Դֱ�ӷ��ء�
        {
            callback(r.asset as T);
        }

    }

}

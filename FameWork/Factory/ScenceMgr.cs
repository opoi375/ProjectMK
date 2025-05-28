using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenceMgr
{
    //单例
    private static ScenceMgr instance;
    public static ScenceMgr Instance => instance ??= new ScenceMgr();
    
    /// <summary>
    /// 加载场景(同步)，并回调(可选)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callBack"></param>
    public void LoadScene(string sceneName,UnityAction callBack= null)
    {
        //加载场景
        SceneManager.LoadScene(sceneName);
        //回调
        callBack?.Invoke();
    }

    /// <summary>
    /// 加载场景(异步),并回调(可选)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callBack"></param>
    public void LoadSceneAsync(string scenceName, UnityAction callBack = null) 
    {
        //协程
        MonoMgr.Instance._StartCoroutine(ReallLoadScenceAsyn(scenceName, callBack));
    }

    private IEnumerator ReallLoadScenceAsyn(string scenceName, UnityAction callBack = null) 
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenceName);
        //得到场景加载的进度
        while (!ao.isDone) 
        {
            //这里可以做一些进度条的显示
            // EventCenter.Instance.EventTrigger("ScenceProgress", ao.progress);
            //这里去更新进度条
            yield return ao.progress;
        }
        //加载完之后，回调
        callBack?.Invoke();
        yield break;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenceMgr
{
    //����
    private static ScenceMgr instance;
    public static ScenceMgr Instance => instance ??= new ScenceMgr();
    
    /// <summary>
    /// ���س���(ͬ��)�����ص�(��ѡ)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callBack"></param>
    public void LoadScene(string sceneName,UnityAction callBack= null)
    {
        //���س���
        SceneManager.LoadScene(sceneName);
        //�ص�
        callBack?.Invoke();
    }

    /// <summary>
    /// ���س���(�첽),���ص�(��ѡ)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callBack"></param>
    public void LoadSceneAsync(string scenceName, UnityAction callBack = null) 
    {
        //Э��
        MonoMgr.Instance._StartCoroutine(ReallLoadScenceAsyn(scenceName, callBack));
    }

    private IEnumerator ReallLoadScenceAsyn(string scenceName, UnityAction callBack = null) 
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenceName);
        //�õ��������صĽ���
        while (!ao.isDone) 
        {
            //���������һЩ����������ʾ
            // EventCenter.Instance.EventTrigger("ScenceProgress", ao.progress);
            //����ȥ���½�����
            yield return ao.progress;
        }
        //������֮�󣬻ص�
        callBack?.Invoke();
        yield break;
    }


}

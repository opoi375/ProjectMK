using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr
{
    //����ģʽ
   private static PoolMgr instance;
   public static PoolMgr Instance => instance ??= new PoolMgr();

   public Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

   private GameObject poolParent;
    /// <summary>
    /// �ӳ��л�ȡ����
    /// </summary>
    /// <returns>����</returns>
    public GameObject GetObject(string name)
    {
        GameObject obj = null;
        //�г��룬�����ж���
        if(poolDic.ContainsKey(name) && poolDic[name].Count > 0) 
        {
            obj = poolDic[name].Dequeue();
        }
        else 
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Fabs/"+name));
        }

        //����
        obj.SetActive(true);
        //�Ͽ����ӹ�ϵ
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
        //��obj�ŵ�poolParent��
        obj.transform.parent = poolParent.transform;


        //�Ѳ��õĶ���Żس���
        if (poolDic.ContainsKey(name)&&poolDic[name].Count >= 0)
        {
            //Debug.Log("�г��룬�Ž�ȥ");
          //�г��룬�Ž�ȥ
            poolDic[name].Enqueue(obj);
        }
        else 
        {
            //Debug.Log("û�г��룬�½�һ��");
            //û�г��룬�½�һ��
            poolDic.Add(name, new Queue<GameObject>());
            //�Ž�ȥ
            poolDic[name].Enqueue(obj);
        }
        //��objʧ��
        obj.SetActive(false);

    }

    /// <summary>
    /// ��ճ��ӵķ��������ڳ����л���ʱ��
    /// </summary>
     public void Clear() 
     {
        //��ճ��ӣ������ڴ�й©
         poolDic.Clear();
         poolParent = null;
     }

}

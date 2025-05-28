using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr 
{
    private static InputMgr instance;
    public static InputMgr Instance => instance ??=new InputMgr();


    private bool isStart = false;
    public InputMgr()
    {
        //��Ӽ����¼�
        MonoMgr.Instance.AddUpdateListener(InputUpdate);
    }

    //��ʼ�ͽ�ֹ����
    public void EnableInput() => isStart = true; 
    public void DisableInput() => isStart = false; 

    /// <summary>
    /// �������,���������¼�
    /// </summary>
    private void InputUpdate()
    {
        if(isStart == false)
        {
            //�������û�б������򲻽����κβ���
            return;
        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            EventCenter.Instance.EventTrigger("W pressed");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventCenter.Instance.EventTrigger("W released");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventCenter.Instance.EventTrigger("A pressed");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            EventCenter.Instance.EventTrigger("A released");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventCenter.Instance.EventTrigger("S pressed");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            EventCenter.Instance.EventTrigger("S released");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EventCenter.Instance.EventTrigger("D pressed");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            EventCenter.Instance.EventTrigger("D released");
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            EventCenter.Instance.EventTrigger("Left mouse button pressed");
        }
        if (Input.GetMouseButtonUp(0))
        {
            EventCenter.Instance.EventTrigger("Left mouse button released");
        }
        if (Input.GetMouseButtonDown(1))
        {
            EventCenter.Instance.EventTrigger("Right mouse button pressed");
        }
        if (Input.GetMouseButtonUp(1))
        {
            EventCenter.Instance.EventTrigger("Right mouse button released");
        }
        if (Input.GetMouseButtonDown(2))
        {
            EventCenter.Instance.EventTrigger("Middle mouse button pressed");
        }
        if (Input.GetMouseButtonUp(2))
        {
            EventCenter.Instance.EventTrigger("Middle mouse button released");
        }
    }
}

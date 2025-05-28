using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    /// <summary>
    /// ��ȡĿ���
    /// </summary>
    /// <returns></returns>
    public Transform GetTopPoint();
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// �������
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// �Ƿ�������
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject();
   
}
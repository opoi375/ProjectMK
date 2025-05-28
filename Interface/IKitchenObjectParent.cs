using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    /// <summary>
    /// 获取目标点
    /// </summary>
    /// <returns></returns>
    public Transform GetTopPoint();
    /// <summary>
    /// 设置物体
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// 获取物体
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// 清除物体
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// 是否有物体
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject();
   
}
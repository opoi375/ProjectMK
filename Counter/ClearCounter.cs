using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter,ISelectable,IKitchenObjectParent
{
    /// <summary>
    /// 正在存放物体
    /// </summary>
    protected KitchenObject kitchenObject;

   public virtual void Interact(Player player)
    {
        //将物体放置到指定的位置
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //将player的物体放到柜台,如果player没有物体，则不执行
                player.GetKitchenObject()?.SetObjectParent(this);
            }
            else 
            {
            
            }
           

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //这个player目前有物体
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player的物体是盘子，将物体放到player的盘子上
                    KitchenObjectPlate plate = player.GetKitchenObject() as KitchenObjectPlate;
                    plate.AddKitchenObject(GetKitchenObject());

                }
                else 
                {
                    player.GetKitchenObject()?.SetObjectParent(this);
                }
            }
            else
            {
                //这个player没有物体，将这个物体给到player
                kitchenObject.SetObjectParent(player);
            }
        }
        

    }

    





    public void Select()
    {
        selectableObj.SetActive(true);
    }
    public void Deselect()
    {
        selectableObj.SetActive(false);
    }
    /// <summary>
    /// 获取顶部的点
    /// </summary>
    /// <returns></returns>
    public virtual Transform GetTopPoint()
    {
        return topPoint;
    }
     /// <summary>
     /// 设置物体
     /// </summary>
     /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject) 
    {
        this.kitchenObject = kitchenObject;
    }
   /// <summary>
   /// 获取物体
   /// </summary>
   /// <returns></returns>
    public KitchenObject GetKitchenObject() 
    {
        return kitchenObject;
    }
    /// <summary>
    /// 清除物体
    /// </summary>
    public void ClearKitchenObject() 
    {
        kitchenObject = null;
    }
    /// <summary>
    /// 是否有物体
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}



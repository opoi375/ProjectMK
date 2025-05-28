using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter,ISelectable
{
   protected const string OPEN = "Open";
    /// <summary>
    /// 要放置的物体
    /// </summary>
    [SerializeField]
    protected KitchenObjectSO kitchenObjectSO;
    /// <summary>
    /// 动画的播放控制器
    /// </summary>
    [SerializeField]
    protected Animator animator;


    public virtual void Interact(Player player)
    {
        if (kitchenObjectSO.prefab == null)
        {
            Debug.LogError("没有指定物体");
            return;
        }
       
        if (!player.HasKitchenObject())
        {
            //如果玩家手上没有物体

            //TODO：动画的播放
            animator.SetTrigger(OPEN);
            //从柜子中拿出菜品
            KitchenObject.SpawnKitcheObject(this.kitchenObjectSO, player);
        }
        else 
        {
            //如果玩家手上有物体
            if(player.GetKitchenObject().GetKitchenObjectSO().objectName == kitchenObjectSO.objectName)
            {
                //如果玩家手上拿着的物体与柜子中的物体相同,则放回到柜子中
                //TODO：动画的播放
                animator.SetTrigger(OPEN);
                //放回到柜子中
                //清除这个物体
                player.GetKitchenObject().DestorySelf();

               
                

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
    public Transform GetTopPoint()
    {
        return topPoint;
    }
    /// <summary>
    /// 设置物体
    /// </summary>
    /// <param name="kitchenObject"></param>

}

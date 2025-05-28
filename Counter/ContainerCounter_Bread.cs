using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContainerCounter_Bread : ContainerCounter
{
    /// <summary>
    /// 用于区分现在去取的是面包
    /// </summary>
    private bool isTop =false;
    [SerializeField]
    private KitchenObjectSO bread_Top;
    [SerializeField]
    private KitchenObjectSO bread_Bottom;

    public override void Interact(Player player)
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
            if (isTop)
            {
                KitchenObject.SpawnKitcheObject(this.bread_Top, player);
            }
            else
            {
                KitchenObject.SpawnKitcheObject(this.bread_Bottom, player);
            }
            //切换状态
            isTop =!isTop;
            
        }
        else
        {
            //如果玩家手上有物体
            if (player.GetKitchenObject().GetKitchenObjectSO().objectName == kitchenObjectSO.objectName)
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
}

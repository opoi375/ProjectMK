using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrashCounter: ContainerCounter
{
    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {
            
        }
        else
        {
            //如果玩家手上有物体
            //清除这个物体
            player.GetKitchenObject().DestorySelf();
            
        }
    }
}

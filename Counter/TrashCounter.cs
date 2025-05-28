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
            //����������������
            //����������
            player.GetKitchenObject().DestorySelf();
            
        }
    }
}

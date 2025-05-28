using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private KitchenObject kitchenObject;


    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public Transform GetTopPoint()
    {
        return transform;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject!= null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void Clear() 
    {
        kitchenObject = null;
        Destroy(gameObject);
        
    }

    //被拾取的逻辑
    private void OnPickup(IKitchenObjectParent parent)
    {
        if (!parent.HasKitchenObject() && (parent as Player).PlayerState == Player.E_PlayerState.Control )
        {
            
            kitchenObject.SetObjectParent(parent);
            Clear();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IKitchenObjectParent>() is Player) 
        {
            Player player = collision.gameObject.GetComponent<Player>();
            //如果碰撞的物体是玩家，则将物品拾取到玩家手上
            if (!player.HasKitchenObject() && player.CanPickUp) 
            {
                //如果玩家没有物品，则将物品拾取到玩家手上
                OnPickup(player);
            }

        }
    }
}

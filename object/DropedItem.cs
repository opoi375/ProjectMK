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

    //��ʰȡ���߼�
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
            //�����ײ����������ң�����Ʒʰȡ���������
            if (!player.HasKitchenObject() && player.CanPickUp) 
            {
                //������û����Ʒ������Ʒʰȡ���������
                OnPickup(player);
            }

        }
    }
}

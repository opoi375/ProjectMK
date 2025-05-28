using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter,ISelectable,IKitchenObjectParent
{
    /// <summary>
    /// ���ڴ������
    /// </summary>
    protected KitchenObject kitchenObject;

   public virtual void Interact(Player player)
    {
        //��������õ�ָ����λ��
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //��player������ŵ���̨,���playerû�����壬��ִ��
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
                //���playerĿǰ������
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player�����������ӣ�������ŵ�player��������
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
                //���playerû�����壬������������player
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
    /// ��ȡ�����ĵ�
    /// </summary>
    /// <returns></returns>
    public virtual Transform GetTopPoint()
    {
        return topPoint;
    }
     /// <summary>
     /// ��������
     /// </summary>
     /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject) 
    {
        this.kitchenObject = kitchenObject;
    }
   /// <summary>
   /// ��ȡ����
   /// </summary>
   /// <returns></returns>
    public KitchenObject GetKitchenObject() 
    {
        return kitchenObject;
    }
    /// <summary>
    /// �������
    /// </summary>
    public void ClearKitchenObject() 
    {
        kitchenObject = null;
    }
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}



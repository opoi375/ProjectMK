using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContainerCounter_Bread : ContainerCounter
{
    /// <summary>
    /// ������������ȥȡ�������
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
            Debug.LogError("û��ָ������");
            return;
        }

        if (!player.HasKitchenObject())
        {
            //����������û������

            //TODO�������Ĳ���
            animator.SetTrigger(OPEN);
            //�ӹ������ó���Ʒ
            if (isTop)
            {
                KitchenObject.SpawnKitcheObject(this.bread_Top, player);
            }
            else
            {
                KitchenObject.SpawnKitcheObject(this.bread_Bottom, player);
            }
            //�л�״̬
            isTop =!isTop;
            
        }
        else
        {
            //����������������
            if (player.GetKitchenObject().GetKitchenObjectSO().objectName == kitchenObjectSO.objectName)
            {
                //�������������ŵ�����������е�������ͬ,��Żص�������
                //TODO�������Ĳ���
                animator.SetTrigger(OPEN);
                //�Żص�������
                //����������
                player.GetKitchenObject().DestorySelf();




            }
        }


    }
}

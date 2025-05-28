using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter,ISelectable
{
   protected const string OPEN = "Open";
    /// <summary>
    /// Ҫ���õ�����
    /// </summary>
    [SerializeField]
    protected KitchenObjectSO kitchenObjectSO;
    /// <summary>
    /// �����Ĳ��ſ�����
    /// </summary>
    [SerializeField]
    protected Animator animator;


    public virtual void Interact(Player player)
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
            KitchenObject.SpawnKitcheObject(this.kitchenObjectSO, player);
        }
        else 
        {
            //����������������
            if(player.GetKitchenObject().GetKitchenObjectSO().objectName == kitchenObjectSO.objectName)
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
    public Transform GetTopPoint()
    {
        return topPoint;
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="kitchenObject"></param>

}

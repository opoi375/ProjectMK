using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class DeliveryCounter : ClearCounter
{
    [SerializeField]
    private Dictionary<string,int> targetObjects= new Dictionary<string, int>();
    private E_DeshType deshType;
    public void Init() 
    {
        
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //���playerĿǰ������
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player�����������ӣ����ڷ������Ӻ���������������岢���ж��Ƿ�ϸ�
                    //TODO:������Ҫ�ж���������������Ƿ�ϸ�
                    CheckPlate(player.GetKitchenObject() as KitchenObjectPlate);
                    //�ύ���ӣ��������ɾ��
                    player.GetKitchenObject().DestorySelf();
                }
                else
                {
                    //player�����岻�����ӣ���ֱ�ӷŵ���̨
                    player.GetKitchenObject()?.SetObjectParent(this);
                }
            }
            else
            {

            }


        }
        else
        {
            if (player.HasKitchenObject())
            {
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
    /// <summary>
    /// ����Ҫ����Ʒ�б�Ÿ�targetObjects
    /// </summary>
    public void GiveTargetObject(Dictionary<string ,int> wantedObjects, E_DeshType deshType=E_DeshType.None)
    {
        this.deshType = deshType;
        targetObjects = wantedObjects;
    }
    public void GiveTargetObject(List<KitchenObjectSO> wantedObjects, E_DeshType deshType=E_DeshType.None)
    {
        //TODO:δ��Ҫʵ�ֵĹ���:ͨ���˿͵����󣬶�̬������Ҫ����Ʒ�б����ŵ�targetObjects�С�
        foreach (var obj in wantedObjects)
        {
            if (this.targetObjects.ContainsKey(obj.objectName))
            {
                this.targetObjects[obj.objectName] += 1;
            }
            else 
            {
                this.targetObjects.Add(obj.objectName, 1);
            }
        }
        this.deshType = deshType;
       
    }

    public void GiveTargetObject() 
    {
        //TODO:ʵ�ִ���˿;Ϳ��Ի�ȡ��Ʒ�б�

    }


    private void CheckPlate(KitchenObjectPlate plate)
    {

        int score = 100;
        List<KitchenObject> objectsInPlate = plate.ObjectList;
        foreach (var obj in objectsInPlate)
        {
            if (targetObjects.ContainsKey(obj.GetKitchenObjectSO().objectName)) 
            {
                targetObjects[obj.GetKitchenObjectSO().objectName]--;
            }
            else 
            {
                targetObjects.Add(obj.GetKitchenObjectSO().objectName, -1);
            }
        }
        //TODO:������Ҫ�ж���������������Ƿ�ϸ�

        foreach (var value in targetObjects.Values)
        {
                score -= Abs(value)*5;
        }
        Debug.Log("������" + score);
        //����Լ�����Ʒ�б�
        targetObjects.Clear();
        //���ﴥ���¼��ò�Ʒˢ��
        EventCenter.Instance.EventTrigger("RefreshTargetDish");
        
       
    }

}

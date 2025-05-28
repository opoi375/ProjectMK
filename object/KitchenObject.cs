using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    /// <summary>
    /// �洢��������Ϣ
    /// </summary>
    [SerializeField]
    protected KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// �Լ�Ŀǰ���ŵ��Ĺ�̨
    /// </summary>
    protected IKitchenObjectParent ObjectParent;


    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    /// <summary>
    /// �����Լ����ŵ��Ĺ�̨
    /// </summary>
    /// <param name="objectParent">��̨</param>
    public virtual void SetObjectParent(IKitchenObjectParent objectParent)
    {
        
        if (objectParent.HasKitchenObject()) 
        {
            //����¹�̨�Ѿ�����Ʒ�������ٷ���
            

            //TODO:�������Ʒ�߼�
            
            if (objectParent.GetKitchenObject() is KitchenObjectPlate && kitchenObjectSO.objectName != "Plate")//��ֹ����������
            {
                //��������п��Է����������Ʒ�����Է���
                KitchenObjectPlate parent = (objectParent.GetKitchenObject() as KitchenObjectPlate);
                parent.AddKitchenObject(this);
            }
            //Debug.LogError("�ù�̨�Ѿ�����Ʒ�������ٷ���");
            return;
            
        }
        if (this.ObjectParent != null)
        {
            //���֮ǰ�й�̨�������֮ǰ����Ʒ
            this.ObjectParent.ClearKitchenObject();
        }
        this.ObjectParent = objectParent;

        //���Լ��Ƶ���һ����̨
        objectParent.SetKitchenObject(this);
        transform.SetParent(objectParent.GetTopPoint());//���ø�����Ϊ��̨����
        transform.localPosition = Vector3.zero; //�������λ��Ϊ0
    }
    /// <summary>
    /// ��ȡ�Լ����ŵ��Ĺ�̨
    /// </summary>
    /// <returns>Ŀǰ���ŵ��Ĺ�̨</returns>
    public IKitchenObjectParent GetObjectParent()
    {
        return ObjectParent;
    }

    /// <summary>
    /// �����Լ�
    /// </summary>
    public virtual void DestorySelf() 
    {
        ObjectParent.ClearKitchenObject();//TODO:ʹ�ö����
        Destroy(gameObject);
    }

    public virtual void ThrowSelf() 
    {
       GameObject obj = ResourcesMgr.Instance.Load<GameObject>("Prefab/DropedItem/DropedItem");
       obj.transform.position = transform.position;
       obj.transform.SetParent(null);
        DropedItem dropedItem = obj.GetComponent<DropedItem>();
        SetObjectParent(dropedItem);

    }


    //������һЩ��̬����

    public static void SpawnKitcheObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent objectParent) 
    {
        Transform obj = Instantiate(kitchenObjectSO.prefab);
        obj.GetComponent<KitchenObject>().SetObjectParent(objectParent);
    }
}

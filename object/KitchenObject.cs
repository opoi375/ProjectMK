using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    /// <summary>
    /// 存储的物体信息
    /// </summary>
    [SerializeField]
    protected KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// 自己目前被放到的柜台
    /// </summary>
    protected IKitchenObjectParent ObjectParent;


    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    /// <summary>
    /// 设置自己被放到的柜台
    /// </summary>
    /// <param name="objectParent">柜台</param>
    public virtual void SetObjectParent(IKitchenObjectParent objectParent)
    {
        
        if (objectParent.HasKitchenObject()) 
        {
            //如果新柜台已经有物品，则不能再放入
            

            //TODO:更多的物品逻辑
            
            if (objectParent.GetKitchenObject() is KitchenObjectPlate && kitchenObjectSO.objectName != "Plate")//防止盘子套盘子
            {
                //如果这里有可以放入的其他物品，则尝试放入
                KitchenObjectPlate parent = (objectParent.GetKitchenObject() as KitchenObjectPlate);
                parent.AddKitchenObject(this);
            }
            //Debug.LogError("该柜台已经有物品，不能再放入");
            return;
            
        }
        if (this.ObjectParent != null)
        {
            //如果之前有柜台，则清除之前的物品
            this.ObjectParent.ClearKitchenObject();
        }
        this.ObjectParent = objectParent;

        //将自己移到另一个柜台
        objectParent.SetKitchenObject(this);
        transform.SetParent(objectParent.GetTopPoint());//设置父物体为柜台顶点
        transform.localPosition = Vector3.zero; //设置相对位置为0
    }
    /// <summary>
    /// 获取自己被放到的柜台
    /// </summary>
    /// <returns>目前被放到的柜台</returns>
    public IKitchenObjectParent GetObjectParent()
    {
        return ObjectParent;
    }

    /// <summary>
    /// 销毁自己
    /// </summary>
    public virtual void DestorySelf() 
    {
        ObjectParent.ClearKitchenObject();//TODO:使用对象池
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


    //以下是一些静态方法

    public static void SpawnKitcheObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent objectParent) 
    {
        Transform obj = Instantiate(kitchenObjectSO.prefab);
        obj.GetComponent<KitchenObject>().SetObjectParent(objectParent);
    }
}

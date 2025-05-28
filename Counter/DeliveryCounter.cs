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
                //这个player目前有物体
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player的物体是盘子，则在放置盘子后检查盘子里面的物体并且判断是否合格
                    //TODO:这里需要判断盘子里面的物体是否合格。
                    CheckPlate(player.GetKitchenObject() as KitchenObjectPlate);
                    //提交盘子，这里就先删除
                    player.GetKitchenObject().DestorySelf();
                }
                else
                {
                    //player的物体不是盘子，则直接放到柜台
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
                    //player的物体是盘子，将物体放到player的盘子上
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
                //这个player没有物体，将这个物体给到player
                kitchenObject.SetObjectParent(player);
            }
        }
    }
    /// <summary>
    /// 把需要的物品列表放给targetObjects
    /// </summary>
    public void GiveTargetObject(Dictionary<string ,int> wantedObjects, E_DeshType deshType=E_DeshType.None)
    {
        this.deshType = deshType;
        targetObjects = wantedObjects;
    }
    public void GiveTargetObject(List<KitchenObjectSO> wantedObjects, E_DeshType deshType=E_DeshType.None)
    {
        //TODO:未来要实现的功能:通过顾客的需求，动态生成需要的物品列表，并放到targetObjects中。
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
        //TODO:实现传入顾客就可以获取物品列表

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
        //TODO:这里需要判断盘子里面的物体是否合格。

        foreach (var value in targetObjects.Values)
        {
                score -= Abs(value)*5;
        }
        Debug.Log("分数：" + score);
        //清空自己的物品列表
        targetObjects.Clear();
        //这里触发事件让菜品刷新
        EventCenter.Instance.EventTrigger("RefreshTargetDish");
        
       
    }

}

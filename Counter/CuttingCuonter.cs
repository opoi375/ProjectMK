using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCuonter : BaseCounter, ISelectable,IKitchenObjectParent,IAlternateInteractable
{
    const string CUT = "Cut";
    [SerializeField]
    protected CuttingRecipeSO[] recipes;
    [SerializeField]
    Animator animator;
    /// <summary>
    /// 这个是进度条
    /// </summary>
    [SerializeField]
    Image processImage;
    private int cuttingprocess = 0;

    /// <summary>
    /// 含有的物体
    /// </summary>
    protected KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (HasKitchenObject()) 
            {
                //都有物体
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player的物体是盘子，将物体放到player的盘子上
                    KitchenObjectPlate plate = player.GetKitchenObject() as KitchenObjectPlate;
                    //TODO:放入盘子
                    plate.AddKitchenObject(GetKitchenObject());

                }
                else
                {
                    player.GetKitchenObject()?.SetObjectParent(this);
                }
            }
            else 
            {
                //只有玩家有物体
                 KitchenObject kobject = player.GetKitchenObject();
                if (kobject is KitchenObjectPlate)
                {
                   
                }
                else 
                {
                    player.GetKitchenObject()?.SetObjectParent(this);
                }
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                //只有柜台有物体
                kitchenObject.SetObjectParent(player);
                //清空进度
                cuttingprocess = 0;
                //恢复进度条
                processImage.fillAmount = 0;
            }
            else
            {
                //都没有物体
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
    /// 获取顶部的点
    /// </summary>
    /// <returns></returns>
    public Transform GetTopPoint()
    {
        return topPoint;
    }
    /// <summary>
    /// 设置物体
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    /// <summary>
    /// 获取物体
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    /// <summary>
    /// 清除物体
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    /// <summary>
    /// 是否有物体
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    /// <summary>
    /// 交互替换的接口
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteactAlternate(Player player = null)
    {
        if (player.HasKitchenObject())
        {
            if (HasKitchenObject())
            {
                //都有物体
            }
            else
            {
                //只有玩家有物体
                
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                //只有柜台有物体
                //TODO:搜索切换表

               var output = GetOutputObject(kitchenObject.GetKitchenObjectSO());
                //如果动画播完了
               if(output != null && animator.GetCurrentAnimatorStateInfo(0).IsName("CuttingCounterIdle"))
                {
                    //累计过程
                    cuttingprocess++;
                    
                    //TODO:添加动画
                    animator.SetTrigger(CUT);
                    //更新进度条
                    processImage.fillAmount = (float)cuttingprocess / GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax;

                    if (cuttingprocess >= GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax)
                    {
                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpawnKitcheObject(output, this);
                        //清零过程
                        cuttingprocess = 0;
                        //恢复进度条
                        processImage.fillAmount = 0;
                    }

                }

            }
            else
            {
                //都没有物体
            }
        }
    }
    /// <summary>
    /// 是否有输出物体
    /// </summary>
    /// <param name="ingredient"></param>
    /// <returns></returns>
    public bool HasOutputObject(KitchenObjectSO ingredient) 
    {
        foreach(var output in recipes) 
        {
            if(ingredient.objectName == output.ingredient.objectName) 
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取食谱中要输出的物体
    /// </summary>
    /// <param name="ingredient"></param>
    /// <returns></returns>
    public KitchenObjectSO GetOutputObject(KitchenObjectSO ingredient)
    {
        foreach(var output in recipes) 
        {
            if(ingredient.objectName == output.ingredient.objectName) 
            {
                return output.result;
            }
        }
        return null;
    }

    public CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO ingredient) 
    {
        foreach(var output in recipes) 
        {
            if(ingredient.objectName == output.ingredient.objectName) 
            {
                return output;
            }
        }
        return null;
    }
}

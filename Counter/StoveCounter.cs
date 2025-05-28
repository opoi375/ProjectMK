using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounter : ClearCounter
{
    [SerializeField]
    private CookingRecipSO[] recips;
    [SerializeField]
    private Image proccessImage; 
    [SerializeField]
    private GameObject stoveOn;

    private float cookingTime = 0f;
   
    private enum E_StoveState 
    {
        StoveOff,
        StoveOn,
    }
    private E_StoveState stoveState = E_StoveState.StoveOff;

    private void Update()
    {
        if (cookingTime != 0)
        {
            stoveState = E_StoveState.StoveOn;
        }
        else 
        {
            stoveState = E_StoveState.StoveOff;
        }


        if (stoveState == E_StoveState.StoveOff) 
        {
            stoveOn.SetActive(false);
        }
        else if (stoveState == E_StoveState.StoveOn)
        {
            stoveOn.SetActive(true);
        }
    }



    /// <summary>
    /// 用于管理烹饪过程的协程
    /// </summary>
    private Coroutine cookingCoroutine;
    public override void Interact(Player player)
    {
        //将物体放置到指定的位置
        if (!HasKitchenObject())
        {
            //将player的物体放到柜台,如果player没有物体，则不执行
            KitchenObject kitchenObject = player.GetKitchenObject();
            if(kitchenObject is KitchenObjectPlate) 
            {
                //盘子不能放入
            }
            else 
            {
                kitchenObject?.SetObjectParent(this);
            }

            //更新时间
            cookingTime = 0;

            if (HasOutputObject(GetKitchenObject()?.GetKitchenObjectSO()))
            {
                //如果可以被烹饪，则开始烹饪
                cookingCoroutine = StartCoroutine(Cooking(GetKitchenObject().GetKitchenObjectSO()));
            }

            

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //这个player目前有物体
            }
            else
            {
                //这个player没有物体，将这个物体给到player
                
                kitchenObject?.SetObjectParent(player);


                //更新进度条
                proccessImage.color = Color.green;
                proccessImage.fillAmount = 0;
                //停止协程
                StopCoroutine(cookingCoroutine);
                //更新时间
                cookingTime = 0;
            }
        }

       
    }


    private IEnumerator Cooking(KitchenObjectSO ingredient) 
    {
       
        //烹饪过程
        while (cookingTime < GetCookingRecipeSO(ingredient).proceessingTime) 
        {
            yield return new WaitForSeconds(0.2f);
            cookingTime += 0.2f;
            //更新进度条
            proccessImage.color = Color.green;
            proccessImage.fillAmount = cookingTime / GetCookingRecipeSO(ingredient).proceessingTime;
        }
        //烹饪完成
        GetKitchenObject().DestorySelf();
        KitchenObject.SpawnKitcheObject(GetOutputObject(ingredient), this);
        


        while (cookingTime < GetCookingRecipeSO(ingredient).failureTime) 
        {
            yield return new WaitForSeconds(0.2f);
            cookingTime += 0.2f;
            //更新进度条
            proccessImage.color = Color.red;
            proccessImage.fillAmount = (cookingTime-GetCookingRecipeSO(ingredient).proceessingTime) /  (GetCookingRecipeSO(ingredient).failureTime -GetCookingRecipeSO(ingredient).proceessingTime);
        }
        //烹饪失败
        GetKitchenObject().DestorySelf();
        KitchenObject.SpawnKitcheObject(GetOutputFailObject(ingredient), this);
        proccessImage.fillAmount = 0;
        yield break;
        
    }


    /// <summary>
    /// 是否有输出物体
    /// </summary>
    /// <param name="ingredient"></param>
    /// <returns></returns>
    public bool HasOutputObject(KitchenObjectSO ingredient)
    {
        if (ingredient == null)
        {
            return false;
        }
        foreach (var output in recips)
        {
            if (ingredient.objectName == output.ingredient.objectName)
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
        foreach (var output in recips)
        {
            if (ingredient.objectName == output.ingredient.objectName)
            {
                return output.result;
            }
        }
        return null;
    }

    public KitchenObjectSO GetOutputFailObject(KitchenObjectSO ingredient) 
    {
        foreach (var output in recips)
        {
            if (ingredient.objectName == output.ingredient.objectName)
            {
                return output.failResult;
            }
        }
        return null;
    }
    public CookingRecipSO GetCookingRecipeSO(KitchenObjectSO ingredient)
    {
        foreach (var output in recips)
        {
            if (ingredient.objectName == output.ingredient.objectName)
            {
                return output;
            }
        }
        return null;
    }


}

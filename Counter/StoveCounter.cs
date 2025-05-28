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
    /// ���ڹ�����⿹��̵�Э��
    /// </summary>
    private Coroutine cookingCoroutine;
    public override void Interact(Player player)
    {
        //��������õ�ָ����λ��
        if (!HasKitchenObject())
        {
            //��player������ŵ���̨,���playerû�����壬��ִ��
            KitchenObject kitchenObject = player.GetKitchenObject();
            if(kitchenObject is KitchenObjectPlate) 
            {
                //���Ӳ��ܷ���
            }
            else 
            {
                kitchenObject?.SetObjectParent(this);
            }

            //����ʱ��
            cookingTime = 0;

            if (HasOutputObject(GetKitchenObject()?.GetKitchenObjectSO()))
            {
                //������Ա���⿣���ʼ���
                cookingCoroutine = StartCoroutine(Cooking(GetKitchenObject().GetKitchenObjectSO()));
            }

            

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //���playerĿǰ������
            }
            else
            {
                //���playerû�����壬������������player
                
                kitchenObject?.SetObjectParent(player);


                //���½�����
                proccessImage.color = Color.green;
                proccessImage.fillAmount = 0;
                //ֹͣЭ��
                StopCoroutine(cookingCoroutine);
                //����ʱ��
                cookingTime = 0;
            }
        }

       
    }


    private IEnumerator Cooking(KitchenObjectSO ingredient) 
    {
       
        //��⿹���
        while (cookingTime < GetCookingRecipeSO(ingredient).proceessingTime) 
        {
            yield return new WaitForSeconds(0.2f);
            cookingTime += 0.2f;
            //���½�����
            proccessImage.color = Color.green;
            proccessImage.fillAmount = cookingTime / GetCookingRecipeSO(ingredient).proceessingTime;
        }
        //������
        GetKitchenObject().DestorySelf();
        KitchenObject.SpawnKitcheObject(GetOutputObject(ingredient), this);
        


        while (cookingTime < GetCookingRecipeSO(ingredient).failureTime) 
        {
            yield return new WaitForSeconds(0.2f);
            cookingTime += 0.2f;
            //���½�����
            proccessImage.color = Color.red;
            proccessImage.fillAmount = (cookingTime-GetCookingRecipeSO(ingredient).proceessingTime) /  (GetCookingRecipeSO(ingredient).failureTime -GetCookingRecipeSO(ingredient).proceessingTime);
        }
        //���ʧ��
        GetKitchenObject().DestorySelf();
        KitchenObject.SpawnKitcheObject(GetOutputFailObject(ingredient), this);
        proccessImage.fillAmount = 0;
        yield break;
        
    }


    /// <summary>
    /// �Ƿ����������
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
    /// ��ȡʳ����Ҫ���������
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

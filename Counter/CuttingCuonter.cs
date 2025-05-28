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
    /// ����ǽ�����
    /// </summary>
    [SerializeField]
    Image processImage;
    private int cuttingprocess = 0;

    /// <summary>
    /// ���е�����
    /// </summary>
    protected KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (HasKitchenObject()) 
            {
                //��������
                if (player.GetKitchenObject() is KitchenObjectPlate)
                {
                    //player�����������ӣ�������ŵ�player��������
                    KitchenObjectPlate plate = player.GetKitchenObject() as KitchenObjectPlate;
                    //TODO:��������
                    plate.AddKitchenObject(GetKitchenObject());

                }
                else
                {
                    player.GetKitchenObject()?.SetObjectParent(this);
                }
            }
            else 
            {
                //ֻ�����������
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
                //ֻ�й�̨������
                kitchenObject.SetObjectParent(player);
                //��ս���
                cuttingprocess = 0;
                //�ָ�������
                processImage.fillAmount = 0;
            }
            else
            {
                //��û������
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
    /// <summary>
    /// �����滻�Ľӿ�
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteactAlternate(Player player = null)
    {
        if (player.HasKitchenObject())
        {
            if (HasKitchenObject())
            {
                //��������
            }
            else
            {
                //ֻ�����������
                
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                //ֻ�й�̨������
                //TODO:�����л���

               var output = GetOutputObject(kitchenObject.GetKitchenObjectSO());
                //�������������
               if(output != null && animator.GetCurrentAnimatorStateInfo(0).IsName("CuttingCounterIdle"))
                {
                    //�ۼƹ���
                    cuttingprocess++;
                    
                    //TODO:��Ӷ���
                    animator.SetTrigger(CUT);
                    //���½�����
                    processImage.fillAmount = (float)cuttingprocess / GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax;

                    if (cuttingprocess >= GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax)
                    {
                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpawnKitcheObject(output, this);
                        //�������
                        cuttingprocess = 0;
                        //�ָ�������
                        processImage.fillAmount = 0;
                    }

                }

            }
            else
            {
                //��û������
            }
        }
    }
    /// <summary>
    /// �Ƿ����������
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
    /// ��ȡʳ����Ҫ���������
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : ClearCounter
{
    [SerializeField]
    private float breakPlateTimer = 4f;
    private int platesCount = 0;
    [SerializeField]
    private int plateCountMax = 4;
    /// <summary>
    /// 所有盘子的栈
    /// </summary>
    private Stack<Transform> plates = new Stack<Transform>();
    [SerializeField]
    private KitchenObjectSO plateSO;
    

    private Coroutine spawnPlateCoroutine;
   
    private void Start() 
    {
        spawnPlateCoroutine = StartCoroutine(SpawnPlateCoroutine());
    }

    private void OnDestroy()
    {
        StopCoroutine(spawnPlateCoroutine);
    }

    private  IEnumerator SpawnPlateCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(breakPlateTimer);
            if (platesCount < plateCountMax)
            {
                //TODO: 生成一个新的Plate
                SpawnPlate();
            }
        }
    
    }

    private void SpawnPlate()
    {
        //TODO:生成盘子的逻辑
        Transform plate = Instantiate(plateSO.prefab, topPoint.position + Vector3.up * 0.05f * platesCount, Quaternion.identity);
        plates.Push(plate);
        platesCount++;

    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {

            //这个player没有物体，将物体交给player
            if (platesCount > 0)
            {
                KitchenObject kitchenObject = plates.Pop().GetComponent<KitchenObject>();
                kitchenObject.SetObjectParent(player);
                platesCount--;
            }
        }
        else if(platesCount < plateCountMax)
        {
            if(player.GetKitchenObject().GetKitchenObjectSO().objectName == plateSO.objectName) 
            {
                player.GetKitchenObject().DestorySelf();
                SpawnPlate();
            }
        }




    }


}

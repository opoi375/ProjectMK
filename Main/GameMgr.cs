using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
   
    void Start()
    {
        UIMgr.Instance.ShowPanel<TargetDishPanel>();   
    }

   
}

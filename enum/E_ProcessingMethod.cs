using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_ProcessingMethod 
{
    None=0,
    Cutting=1,
    Cooking=2,

    
}

public static class Extension
{
    public static bool Contains(this E_ProcessingMethod processingMethod, E_ProcessingMethod target) 
    {
        return (processingMethod & target) == target;
    }

    public static bool IsNone(this E_ProcessingMethod processingMethod) 
    {
        return processingMethod == E_ProcessingMethod.None;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlternateInteractable
{
    /// <summary>
    /// 交互代替的接口
    /// </summary>
    public void InteactAlternate(Player player = null);

}

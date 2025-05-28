using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 交互接口
public interface IInteractable
{
    //交互的具体逻辑
    void Interact(Player player);
}
// 选中接口，一般直接继承这个接口就可以了
public interface ISelectable : IInteractable
{
    //选中的具体逻辑
    void Select()
    {
        //显示选中效果
    }
    //取消选中的具体逻辑
    void Deselect();

}

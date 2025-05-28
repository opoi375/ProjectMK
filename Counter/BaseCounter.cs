using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    /// <summary>
    /// 被选中时显示的物体
    /// </summary>
    [SerializeField]
    protected GameObject selectableObj;
    /// <summary>
    /// 顶部的点
    /// </summary>
    [SerializeField]
    protected Transform topPoint;
}

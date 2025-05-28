using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface IHurt
{
    /// <summary>
    /// 受伤的接口，会和受伤动画接口一起使用
    /// </summary>
    /// <param name="atk"></param>
    public void Hurt(IAtk atk);
    /// <summary>
    /// 受伤动画的接口
    /// </summary>
    public void HurtAnimation();
    
}


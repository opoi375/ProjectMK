using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface IAtk
{
  
    /// <summary>
    /// 攻击目标的接口
    /// </summary>
    /// <param name="target"></param>
    public void DoAttack(IHurt target);
}


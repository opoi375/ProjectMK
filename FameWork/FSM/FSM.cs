using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public IState curState;

    public Dictionary<StateType, IState> states;

    public Blackborad blackborad;
    public FSM(Blackborad blackborad)
    {
        this.states = new Dictionary<StateType, IState>();
        this.blackborad = blackborad;

    }
    /// <summary>
    /// 添加角色的状态
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="state"></param>
    public void AddState(StateType stateType, IState state) 
    {
        if( states.ContainsKey(stateType)) 
        {
            Debug.Log("已经存在该状态");
            return;
        }
        states.Add(stateType, state);
    }
    /// <summary>
    /// 切换状态的逻辑
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(StateType state) 
    {
        if (!states.ContainsKey(state)) 
        {
            Debug.Log("不存在该状态");
            return;
        }
        if (curState!= null) 
        {
            curState.OnExit();
        }
        curState = states[state];
        curState.OnEnter();
    }

    public void OnUpdate() 
    {
        curState?.OnUpdate();
    }

}
[System.Serializable]
public class Blackborad 
{
    //此处是用于
}

public enum StateType
{
    Idle,
    Walk,
    Run,
    Attack,
    Dead,
    Kill,
}

public interface IState
{
    void OnEnter();
    void OnExit();
    void OnUpdate();

}



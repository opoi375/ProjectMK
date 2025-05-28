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
    /// ��ӽ�ɫ��״̬
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="state"></param>
    public void AddState(StateType stateType, IState state) 
    {
        if( states.ContainsKey(stateType)) 
        {
            Debug.Log("�Ѿ����ڸ�״̬");
            return;
        }
        states.Add(stateType, state);
    }
    /// <summary>
    /// �л�״̬���߼�
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(StateType state) 
    {
        if (!states.ContainsKey(state)) 
        {
            Debug.Log("�����ڸ�״̬");
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
    //�˴�������
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



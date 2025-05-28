using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jaeger : MonoBehaviour, IAtk
{
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private float normalspeed = 3f;
    [SerializeField]
    private float rushspeed = 5f;
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private Animator animator;

    private E_JaegerState state;

    private Coroutine coroutine;
    [SerializeField]
    private float atkValue = 10f;

   public enum E_JaegerState 
    {
        Idel,
        Chase,
        Attack,
        Kill,
        Stop,
        CheckKill
    }

    private void Start()
    {
       Init();
        
    }

    private void Update()
    {
        switch (state) 
        {
            case E_JaegerState.Idel:
                Idle();
                break;
            case E_JaegerState.Chase:
                Chase();
                break;
            case E_JaegerState.Attack:
                Attack();
                break;
            case E_JaegerState.Kill:
                Kill();
                break;
            case E_JaegerState.Stop:
                Stop();
                break;
            case E_JaegerState.CheckKill:
                CheckKill();
                break;
            default:
                break;
        }
       
    }

    public void Init() 
    {

        state = E_JaegerState.Idel;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        ChangeTarget(FindObjectOfType<Player>().transform);
        particleSystem.Stop();
        
    }
    private void Idle()
    {
        animator.SetBool("IsMove", false);
    }

    private void Chase()
    {
        //开启导航
        agent.isStopped = false;
        animator.SetBool("IsMove",true);
        if (target != null)
        {
            agent.speed = normalspeed;
            agent.SetDestination(target.position);
            if(target.GetComponent<Player>().IsDead == true) 
            {
                //TODO:更换目标

            }
        }
        else
        {
            Debug.Log("未找到目标");
        }

        if (agent.remainingDistance < 7f)
        {
            
            state = E_JaegerState.Attack;
        }
    }

    private void Attack()
    { 
        //攻击
        transform.LookAt(target);
        coroutine =  StartCoroutine(AttackCoroutine());
        state = E_JaegerState.CheckKill;//防止多次触发协程

    }

    private IEnumerator AttackCoroutine() 
    {
        //加速
        agent.speed = rushspeed;
        yield return new WaitForSeconds(0.5f);
        //停止冲刺
        agent.speed = normalspeed;
        if (state == E_JaegerState.CheckKill)
        {
            state = E_JaegerState.Chase;
        }


    }
    private void Kill() 
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(KillCoroutine());
        state = E_JaegerState.Stop;


    }

    private IEnumerator KillCoroutine() 
    {
        hand.transform.position = target.position;
        Player player = target.GetComponent<Player>();
        DoAttack(player);
        player.ChangePlayerState(Player.E_PlayerState.notControl);
        //TODO: 实现击杀效果动画
        //设置父子物体
        target.SetParent(hand);
        target.transform.localPosition= Vector3.zero;
        animator.SetTrigger("Kill");
        yield return new WaitForSeconds(1.7f);
        //将玩家扔出去
        target.SetParent(null);
        player.Die();

        
        
        agent.destination = new Vector3(0, 0, 0);
        agent.isStopped = false;
        

    }

    private void Stop()
    {

    }

    
    private void CheckKill()
    {
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            state = E_JaegerState.Kill;
        }
    }

    public void DoAttack(IHurt target)
    {
        target.Hurt(this);
    }


    public void ChangeTarget(Transform newTarget = null)
    {
        //更换一个新的目标
        if (newTarget != null) 
        {
            target = newTarget;
        }
        else 
        {
            //如果没有传入目标
            //自动寻找一个目标
            //TODO:寻找目标的具体逻辑

        }
       
    }

    public void StratChase()
    {
       state = E_JaegerState.Chase;
        
    }
   
}

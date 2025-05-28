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
        //��������
        agent.isStopped = false;
        animator.SetBool("IsMove",true);
        if (target != null)
        {
            agent.speed = normalspeed;
            agent.SetDestination(target.position);
            if(target.GetComponent<Player>().IsDead == true) 
            {
                //TODO:����Ŀ��

            }
        }
        else
        {
            Debug.Log("δ�ҵ�Ŀ��");
        }

        if (agent.remainingDistance < 7f)
        {
            
            state = E_JaegerState.Attack;
        }
    }

    private void Attack()
    { 
        //����
        transform.LookAt(target);
        coroutine =  StartCoroutine(AttackCoroutine());
        state = E_JaegerState.CheckKill;//��ֹ��δ���Э��

    }

    private IEnumerator AttackCoroutine() 
    {
        //����
        agent.speed = rushspeed;
        yield return new WaitForSeconds(0.5f);
        //ֹͣ���
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
        //TODO: ʵ�ֻ�ɱЧ������
        //���ø�������
        target.SetParent(hand);
        target.transform.localPosition= Vector3.zero;
        animator.SetTrigger("Kill");
        yield return new WaitForSeconds(1.7f);
        //������ӳ�ȥ
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
        //����һ���µ�Ŀ��
        if (newTarget != null) 
        {
            target = newTarget;
        }
        else 
        {
            //���û�д���Ŀ��
            //�Զ�Ѱ��һ��Ŀ��
            //TODO:Ѱ��Ŀ��ľ����߼�

        }
       
    }

    public void StratChase()
    {
       state = E_JaegerState.Chase;
        
    }
   
}

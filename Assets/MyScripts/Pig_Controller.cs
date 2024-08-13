using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//敵人AI狀態
public enum EnemyState
{
    Idle, //待機
    Move, //移動
    Pursue, //追擊
    Attack, //攻擊
    Hurt, //受傷
    Dead //死亡
}

//野豬AI
public class Pig_Controller : ObjectBase
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckCollider checkCollider;

    //行動範圍
    public float maxX = 4.74f;
    public float minX = -5.63f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;
    //當狀態修改時，進入新狀態要做的拾取
    //相當於OnEnter
    private EnemyState EnemyState
    {
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:

                    break;
                case EnemyState.Move:

                    break;
                case EnemyState.Pursue:

                    break;
                case EnemyState.Attack:

                    break;
                case EnemyState.Hurt:

                    break;
                case EnemyState.Dead:

                    break;
            }
        }
    }

    private void Start()
    {
        Hp = 100;
    }
    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(0);
    }
    protected override void Dead()
    {
        base.Dead();
        PlayAudioDead(0);
        StartCoroutine(DestroyAfterAudio());
    }
    private IEnumerator DestroyAfterAudio()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

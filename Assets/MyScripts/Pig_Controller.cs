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
    private Vector3 targetPos;

    private EnemyState enemyState;
    //這裡就是動畫狀態機
    //當狀態修改時，進入新狀態要做的拾取
    //相當於OnEnter，也就是一進入要做的事
    private EnemyState EnemyState
    {
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    //播放動畫
                    //關閉導航
                    //歇息一段時間再去巡邏，等於一直循環
                    animator.CrossFadeInFixedTime("Idle", 0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));
                    break;
                case EnemyState.Move:
                    //播放動畫
                    //開啟導航
                    //獲取巡邏點
                    //移動到指定目標位置
                    animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    targetPos = GetTargetPos();
                    navMeshAgent.SetDestination(targetPos);
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
        EnemyState = EnemyState.Idle;
    }
    private void Update()
    {
        //更新
        StateOnUpdate();
    }
    //用於每一幀更新時根據當前狀態執行相應的行為
    //因此 StateOnUpdate 方法通常會在 Update 方法中被調用
    private void StateOnUpdate()
    {
        
        switch (enemyState)
        {
            case EnemyState.Idle:

                break;
            case EnemyState.Move:
            //判斷到達時（頭、尾），小於1.5f就待機
            //1.5f是單位距離
                if (Vector3.Distance(transform.position, targetPos) < 1.5f)
                {
                    EnemyState = EnemyState.Idle;
                }
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
    private void GoMove()
    {
        EnemyState = EnemyState.Move;
    }
    //獲取一個範圍內的隨機點
    //獲取vector3直接return值就可以了
    private Vector3 GetTargetPos()
    {
        return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

}

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
    void Awake()
    {
        // // 初始化 animator 和 navMeshAgent元件
        // //這是我之前習慣的用法，在代碼裡面添加好。現在我看正確的用法都是[SerializeField]到面板中自行添加，這樣比較好協作跟後續修改
        // animator = GetComponent<Animator>();
        // navMeshAgent = GetComponent<NavMeshAgent>();

        // // 檢查是否成功獲取
        // if (animator == null)
        // {
        //     Debug.LogError("Animator component not found!");
        // }
        // if (navMeshAgent == null)
        // {
        //     Debug.LogError("NavMeshAgent component not found!");
        // }
    }

    //行動範圍
    public float maxX = 4.74f;
    public float minX = -5.63f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    //目標位置
    private Vector3 targetPos;

    private EnemyState enemyState;
    //將enemyState改為屬性（原本是變量），下方可以設置get和set狀態變化時的行為
    //這裡就是動畫狀態機
    //當狀態修改時，進入新狀態要做的拾取
    //相當於OnEnter，也就是一進入要做的事，只觸發一次
    private EnemyState EnemyState
    {
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState)
            {   //當EnemyState是Idle時
                case EnemyState.Idle:
                    //播放動畫，CrossFadeInFixedTime是淡入，比較絲滑
                    //關閉導航
                    //歇息一段時間再去巡邏，等於一直循環
                    animator.CrossFadeInFixedTime("Idle", 0.25f);
                    navMeshAgent.enabled = false;
                    //2-6喚醒走動
                    Invoke(nameof(GoMove), Random.Range(2f, 6f));
                    break;
                //當EnemyState是Move時
                case EnemyState.Move:
                    //播放動畫
                    //開啟導航
                    //獲取巡邏點（目標位置）
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
    //再把即時更新的地方拆出來，這樣區分初始常態狀態和需要即時更新的部分
    //因此 StateOnUpdate 方法通常會在 Update 方法中被調用
    private void StateOnUpdate()
    {

        switch (enemyState)
        {
            case EnemyState.Idle:

                break;
            case EnemyState.Move:
                //判斷到達時（頭、尾），小於2.5f就待機
                //2.5f是單位距離
                if (Vector3.Distance(transform.position, targetPos) < 2.5f)
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
    //獲取有限範圍內的隨機點
    //獲取vector3直接return值就可以了
    private Vector3 GetTargetPos()
    {
        return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

}

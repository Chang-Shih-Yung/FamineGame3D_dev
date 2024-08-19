using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PlayerController繼承ObjectBase，這裡的protected方法可以被PlayerController使用
public class PlayerController : ObjectBase
{
    //單例模式，这个 instance 变量确保 PlayerController 类只有一个实例，並且可以全局訪問
    public static PlayerController instance;
    //初始化：使用Animator這個元件
    [SerializeField] Animator animator;

    //初始化：可以在面板拖拉使用CheckCollider這個元件
    [SerializeField] CheckCollider checkCollider;

    //初始化：使用CharacterController這個元件
    [SerializeField] CharacterController characterController;

    //這是一個四元數，用來存儲目標方向的旋轉：考量之後還會在其他動作重複使用，所以放在外面
    private Quaternion targetDirRotation;
    [SerializeField] float moveSpeed = 1.5f;

    //初始化是否為攻擊狀態
    private bool isAttacking = false;

    //是否在受傷中
    private bool isHurting = false;


    private float hungry = 100;
    //重構：將hungry改為屬性   
    public float Hungry
    {
        get => hungry;
        set
        {
            hungry = value;
            if (hungry <= 0)
            {
                //避免負數，小於0就是0
                hungry = 0;
                //hungry歸零，開始以每1秒1/2單位慢慢扣生命
                Hp -= Time.deltaTime / 2;
            }
            //更新UI
            //100寫死
            //fillAmount是Image的屬性，0-1之間，所以hungry/100，定義他有100格
            HungryImage.fillAmount = hungry / 100;
        }
    }
    //抓UI
    //這兩個都跟角色有關，HpImage這裡扣的血是跟飢餓度有關，所以也寫在這(被野豬攻擊扣的血量不一樣)
    [SerializeField] Image HungryImage;
    [SerializeField] Image HpImage;


    void Awake()
    {   //this的意思是這個物件，這個物件是PlayerController
        instance = this;
        //初始化身上的攻擊檢測器，知道this是玩家 、然後給予30點傷害
        if (checkCollider != null)
        {   //this=PlayerController，30是傷害值
            checkCollider.Init(this, 30);
        }
        else
        {
            Debug.LogError("CheckCollider not found in children");
        }

    }
    private void Update()
    {
        UpdateHungry();
        //假設不是攻擊狀態，同時也不是受傷狀態，才能移動或發起攻擊
        if (!isAttacking && !isHurting)
        {
            //可以移動可以觸發攻擊事件
            Moving();
            Attack();
        }
        //攻擊過程中
        else
        {
            //角色本人會一直轉向～直到攻擊結束:localRotation是相對於父物件的旋轉，而rotation是相對於世界的旋轉
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetDirRotation, Time.deltaTime * 10);
        }
        // 讓角色的 Y 軸位置回到 0，避免角色飄浮
        Vector3 position = transform.position;
        position.y = Mathf.Lerp(position.y, 0, 1f);
        transform.position = position;
    }
    //檢測攻擊事件
    public void Attack()
    {
        //檢測滑鼠按攻擊，不用Down，也就是持續壓著滑鼠左鍵就會一直攻擊
        if (Input.GetMouseButton(0))
        {

            //轉向到攻擊點~~假如射線檢測到了:相機到滑鼠點的射線(input/out)->射線檢測的最大距離->檢測到Ground層而已
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, layerMask: LayerMask.GetMask("Ground")))
            {
                //播放攻擊動畫
                animator.SetTrigger("Attack");
                //進入攻擊狀態
                isAttacking = true;
                //轉向目標：四元數.LookRotation(目標位置-自己位置)
                targetDirRotation = Quaternion.LookRotation(hit.point - transform.position);

            }
        }
    }
    private void Moving()
    {   //獲取水平和垂直輸入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //將水平和垂直輸入轉換成 向量
        Vector3 move = new Vector3(h, 0, v);
        //處理實際移動以及跟動畫狀態機的連結
        //if (move != Vector3.zero)
        if (move != Vector3.zero)
        {
            //抓動畫狀態機裡面的布爾值名稱，當移動時，walk布爾值為true，然後就會從idle到walk
            animator.SetBool("Walk", true);
            //轉向問題，獲取最終目標方向的旋轉
            targetDirRotation = Quaternion.LookRotation(move);
            //從起點到終點的旋轉，然後用slerp插值，使轉向更加平滑
            //当前对象的旋转，目标旋转，插值
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirRotation, 0.02f);

            //處理實際移動(moveSpeed不寫死，可以在面板中調整)
            //SimpleMove搭配normalized歸一化
            // characterController.SimpleMove(move.normalized * moveSpeed);

            //Move寫法，更有物理狀態一些，我自己比較愛這個移動的感覺，但是重力問題就不會被解決，所以要額外自己加上重力
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
        else
        {
            //抓動畫狀態機裡面的布爾值名稱
            animator.SetBool("Walk", false);
        }
    }


    //計算飢餓值
    //只有這個角色會有飢餓值，所以放在這裡
    private void UpdateHungry()
    {
        //衰減飢餓值，每秒3單位減少
        //Hungry的單位又分成100格
        Hungry -= Time.deltaTime * 3;

    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        //播放受傷動畫
        animator.SetTrigger("Hurt");
        PlayAudio(2);
        isHurting = true;
    }

    //當hp變化時自動調用，很多時候都會觸發hp更新，所以寫在別的地方，這邊調用
    //Hp是ObjectBase裡面的屬性，所以這邊可以調用
    protected override void OnHpUpdate()
    {
        //更新血條UI
        HpImage.fillAmount = Hp / 100;
    }


    //動畫中安插的事件
    //在動畫編輯器中加上的攻擊事件，在這就是調用了這個方法

    private void StartHit()
    {
        //播放音效
        PlayAudio(0);
        //攻擊檢測
        checkCollider.StartHit();

    }

    private void StopHit()
    {
        //停止攻擊檢測
        checkCollider.StopHit();
        //攻擊結束，回到原本的狀態
        isAttacking = false;
    }

    private void HurtOver()
    {
        isHurting = false;
    }
}

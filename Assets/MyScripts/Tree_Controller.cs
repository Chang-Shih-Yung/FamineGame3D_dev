using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//樹控制器
public class Tree : ObjectBase
{
    [SerializeField] Animator animator;
    private void Start()
    {
        //設定樹的血量
        //來自基類ObjectBase的HP定義，這邊直接複用
        Hp = 100;
    }
    public override void Hurt(int damage)
    {
        //base是指ObjectBase這個基類，不改動
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(0);
    }
    protected override void Dead()
    {
        //base是指ObjectBase這個基類，有什麼調用什麼，不改動
        //所以也就傳遞了實例化樹枝且掉落的方法
        base.Dead();

        //以下是在這裡新增的功能
        //播放死亡音效
        PlayAudioDead(0);
        //延遲播放死亡音效
        //StartCoroutine是協程，可以用來延遲執行
        //不然就是invoke
        StartCoroutine(DestroyAfterAudio());
    }
    //銷毀遊戲物件在播放音效後
    //IEnumerator是協程，可以用來延遲執行
    private IEnumerator DestroyAfterAudio()
    {
        // 確保音效播放完成，可以根據音效的時長設置等待時間
        // 這裡假設死亡音效的時長為2秒
        yield return new WaitForSeconds(0.5f);
        // 銷毀遊戲物件
        Destroy(gameObject);
    }

}

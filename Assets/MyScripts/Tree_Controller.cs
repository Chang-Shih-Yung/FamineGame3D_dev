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
        //base是指ObjectBase這個基類，不改動
        base.Dead();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

//專門用來檢測的碰撞物體
public class CheckCollider : MonoBehaviour
{
    //初始化
    //讓碰撞物體知道是誰打的，取用ObjectBase的變量
    //Owner是攻擊者
    private ObjectBase Owner;
    //傷害值
    private int damage;
    private bool canAttack = false;
    //敵人的標籤list
    [SerializeField] List<string> enemyTags = new List<string>();
    //可被拾取的標籤list
    [SerializeField] List<string> itemTags = new List<string>();

    //初始化方法，可以從其他腳本傳入攻擊者和傷害值，目前是在角色底下，所以從PlayerController給值傳入
    //如果放在怪物物件底下，那到時傳進來的就是怪物和傷害值，所以可以知道這個碰撞傷害腳本是可複用的
    //這個init方法是給PlayerController使用的，所以是public
    public void Init(ObjectBase owner, int damage)
    {
        //this的作用：區分變數和成員變數，有this的是成員變數、沒有this的是區域變數
        this.damage = damage;
        //啊不然前面的就改名字，這樣就不會有混淆
        Owner = owner;
    }
    //開啟傷害檢測
    public void StartHit()
    {
        canAttack = true;
    }
    //關閉傷害檢測
    public void StopHit()
    {
        canAttack = false;
        //清空傷害過的物件列表，回到初始狀態，這樣再次攻擊才會被觸發
        hitList.Clear();
    }
    //建立傷害過的物件列表
    private List<GameObject> hitList = new List<GameObject>();

    //碰撞、傷害檢測方法
    //other是碰撞到的物件
    //OnTriggerStay是一個碰撞事件，當碰撞到的物件停留在碰撞範圍內時，就會觸發這個事件
    private void OnTriggerStay(Collider other)
    {

        //可以攻擊的條件
        if (canAttack)
        {
            //此次傷害還沒有檢測過的單位 && 敵人標籤列表中的物件標籤
            //講簡單點：如果碰撞到的物件不在清單中，且是有敵人標籤的話
            if (!hitList.Contains(other.gameObject) && enemyTags.Contains(other.tag))
            {
                //加進去傷害過的清單（例如揮刀比較慢，攻擊未結束但還在攻擊範圍內，此時就不會一直重複檢測、攻擊）
                hitList.Add(other.gameObject);
                //傷害邏輯，獲得ObjectBase組件然後調用ObjectBase中的Hurt基類方法
                //這裡意思很明白，到時候碰到的物件（遊戲怪物）就調用Hurt方法，傷害值是damage。所以在ObjectBase扣的HP到時就是遊戲怪物
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }

        //檢測拾取物品
        //看unity控制面板中itemTag欄位的有哪些tags
        if (itemTags.Contains(other.tag))
        {
            //調用ObjectBase中的PlayAudio方法，播放音效。告訴宿主播放音效
            //這裡就是寫死
            Owner.PlayAudio(1);
            Destroy(other.gameObject);//銷毀撿到的物品

        }
    }
}


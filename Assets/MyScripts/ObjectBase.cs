using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ObjectBase是所有物件的核心基底類別，無指定對象～供有繼承的大家使用
public class ObjectBase : MonoBehaviour
{

    //初始化：使用AudioSource這個元件
    [SerializeField] AudioSource audioSource;
    //列表：存放音效
    [SerializeField] List<AudioClip> audioClips;


    public GameObject lootObject; //掉落物品

    [SerializeField] float hp;
    //重構：將hp改為屬性Hp，其他地方可以直接使用包含邏輯的Hp
    //核心觀念：當hp修改時，會觸發set方法，進而觸發Dead方法，也就是死亡/hp更新
    public float Hp
    {   
        //抓取原始變量，拿來加上一些自己的邏輯
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
            OnHpUpdate();


        }
    }


    //PlayerController繼承ObjectBase，所以這裡的protected方法可以被PlayerController使用
    public void PlayAudio(int index)
    {
        //PlayOneShot播放一次音效，不會被打斷
        //audioClips[index]是從列表中選擇音效
        audioSource.PlayOneShot(audioClips[index]);

    }

    //死亡音效  
    protected virtual void PlayAudioDead(int index)
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    //當hp變化時自動調用
    //PlayerController中的OnHpUpdate方法，就是繼承這個方法～
    //當然PlayerController中的方法會用這裡的方法下去疊加（假設這裡有定義一些的話）
    protected virtual void OnHpUpdate()
    {

    }



    //virtual是虛擬方法，可以被繼承的類重寫
    protected virtual void Dead()
    {
        if (lootObject != null)
        {
            //實例化物體，讓樹枝從上方掉落
            Instantiate(lootObject,
            //隨機範圍位置
            transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f), Random.Range(-0.5f, 0.5f)),
            Quaternion.identity);
        }
    }

    //受傷方法，在碰撞檢測腳本那邊調用
    public virtual void Hurt(int damage)
    {
        Hp -= damage;

    }
    //撿到東西，要知道是什麼 
    //到角色控制那邊調用
    public virtual bool AddItem(ItemType itemType)
    {   
        //預設是false
        return false;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{

    //初始化：使用AudioSource這個元件
    [SerializeField] AudioSource audioSource;
    //列表：存放音效
    [SerializeField] List<AudioClip> audioClips;
    public GameObject lootObject;

    [SerializeField] float hp;
    //重構：將hp改為屬性
    //核心觀念：當hp修改時，會觸發set方法，進而觸發Dead方法，也就是死亡/hp更新
    public float Hp
    {
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
    protected void PlayAudio(int index)
    {
        //PlayOneShot播放一次音效，不會被打斷
        //audioClips[index]是從列表中選擇音效
        audioSource.PlayOneShot(audioClips[index]);

    }
    //當hp變化時自動調用
    //PlayerController中的OnHpUpdate方法，可以傳遞傳重寫的方法回來
    //當然PlayerController中的方法會跟這裡疊加（假設這裡有定義一些的話）
    protected virtual void OnHpUpdate()
    {

    }



    //virtual是虛擬方法，可以被繼承的類重寫
    protected virtual void Dead()
    {
        //播放死亡音效
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//記得引入這個
using UnityEngine.EventSystems;

//後面兩個是：滑鼠進入、滑鼠退出
public class UI_BagPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image bg;
    [SerializeField] Image icon;
    public ItemDefine itemDefine;
    private bool isSelect = false;
    public bool IsSelect
    {
        get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect)
            {   //color是
                bg.color = Color.green;
            }
            else
            {
                bg.color = Color.white;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }

    //初始化：如果傳一個null過來，相當於空格子邏輯
    //來自物品管理器的物品定義
    public void Init(ItemDefine itemDefine = null)
    {
        this.itemDefine = itemDefine;
        isSelect = false;
        if(this.itemDefine == null)
        {
            //如果是空的，就不顯示
            //SetActive是啟用/禁用物件
            icon.gameObject.SetActive(false);
        }
        else
        {   
            //不是空的，就顯示
            icon.gameObject.SetActive(true);
            //顯示物品圖標（來自物品控制腳本）
            icon.sprite = itemDefine.ItemIcon;
        }
    }
}


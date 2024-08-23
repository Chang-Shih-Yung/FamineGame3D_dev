using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//記得引入這個
using UnityEngine.EventSystems;

//後面兩個是：滑鼠進入、滑鼠退出
//UI_BagPanelItem整個就是一個白匡＋內容物件的預製體
public class UI_BagPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image bg;
    [SerializeField] Image icon;
    //來自物品管理器的物品定義
    //public意味著父組件 UI_BagPanel 中的for循環可以直接訪問
    public ItemDefine thisItemDefine;
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

    //初始化：一開始是空的。如果傳一個null過來，相當於空格子邏輯(物件訊息（itemDefine）是空的、圖片（icon）也是空的)
    //來自物品管理器的物品定義(引數itemDefine)
    public void Init(ItemDefine itemDefine = null)
    {
        //thisItemDefine會等到東西(itemDefine就是將ItemManager的GetItemDefine變數傳進來)傳進來賦值
        thisItemDefine = itemDefine;
        isSelect = false;
        if(thisItemDefine == null)
        {
            //如果是空的，就不顯示，
            //gameObject.SetActive是啟用/禁用物件
            icon.gameObject.SetActive(false);
        }
        else
        {   
            //不是空的，就顯示，指的是顯示這個預製體
            icon.gameObject.SetActive(true);
            
            //顯示物品圖標（來自物品控制腳本）
            //sprite是Unity中的一種圖片（Image）類型
            icon.sprite = itemDefine.ItemIcon;
        }
    }
}


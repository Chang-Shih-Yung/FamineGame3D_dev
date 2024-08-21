using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//UI背包
public class UI_BagPanel : MonoBehaviour
{   
    //單例模式
    public static UI_BagPanel instance;
    //有幾個格子（背景格子＋內容物）
    private UI_BagPanelItem[] items;
    private void Awake()
    {
        instance = this; //賦值
    }
    /// <summary>
    /// 添加物品
    /// </summary>
    /// 邏輯是：當拾取物品時，依照物品傳入的物品類型，去物品管理器中拿到物品定義，然後添加對應的物品內容到背包中
    public bool AddItem(ItemType itemType)
    {
        //查看一次，有沒有空格子
        for (int i = 0; i < items.Length; i++)
        {
            //第i個格子里面有沒有物品～取用UI_BagPanelItem中的物品與其定義（內容物）
            //這是個空格子
            if (items[i].itemDefine == null)
            {
                //是空的，就給物品
                //取得某個物品的內容
                ItemDefine itemDefine = ItemManager.instance.GetItemDefine(itemType);
                //然後把物品放進格子（看UI_BagPanelItem）
                items[i].Init(itemDefine);
               return true;
            }
        }
        return false;
    }
}

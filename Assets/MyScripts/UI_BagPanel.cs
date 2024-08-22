using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//UI背包
public class UI_BagPanel : MonoBehaviour
{
    //單例模式
    public static UI_BagPanel instance;

    //格子數量（就是代表整個UI_BagPanelItem）
    private UI_BagPanelItem[] items;

    //實例化UI_BagPanelItem：在面板中把整個UI_BagPanelItem變成預製體
    [SerializeField] GameObject itemPrefab;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //開始就生成格子，然後默認有一個篝火
        //默認篝火
        //Instantiate是生成物體的方法(要生成的物體, 生成到哪裡)
        //一開始先new一個空間
        items = new UI_BagPanelItem[5];
        //取得預製體>>>包含取得BagPanelItem預製體＋取得BagPanelItem預製體上面的BagPanelItem腳本
        UI_BagPanelItem item = Instantiate(itemPrefab, transform).GetComponent<UI_BagPanelItem>();
        //取得篝火訊息>>從ItemManager那邊
        item.Init(ItemManager.instance.GetItemDefine(ItemType.Campfire));
        //第一格就放篝火
        items[0] = item;

        //往後生成空格、放入物件的迴圈
        //數量就是enum中的物件數量
        //篝火佔一個位置，所以從1開始
        for (int i = 1; i < 5; i++)
        {
            item = Instantiate(itemPrefab, transform).GetComponent<UI_BagPanelItem>();
            //生成空格
            item.Init(null);
            //然後放物體進去
            items[i] = item;
        }
    }
    /// <summary>
    /// 添加物品邏輯
    /// </summary>
    /// 邏輯是：布爾判斷受否可以撿，可以的話：當拾取物品時，依照物品傳入的物品類型（引數：itemType），去物品管理器中拿到物品定義(itemDefine)，然後添加對應的物品內容到背包中
    public bool AddItem(ItemType itemType)
    {
        //查看一次，有沒有空格子
        for (int i = 0; i < items.Length; i++)
        {
            //第i個格子里面有沒有物品～取用UI_BagPanelItem中的物品與其定義（內容物）
            //這是個空格子
            if (items[i].thisItemDefine == null)
            {
                //是空的，就給物品>>取得某個物品的內容(來自ItemManager定義的GetItemDefine方法:傳入類型獲得內容)>>然後把物品放進（init）格子（看UI_BagPanelItem）
                ItemDefine GetItemDefine = ItemManager.instance.GetItemDefine(itemType);
                items[i].Init(GetItemDefine);
                return true;
            }
        }
        //沒有空格的話，就返回false
        return false;
    }
}

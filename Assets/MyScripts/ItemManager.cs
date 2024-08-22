using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//物品類型：枚舉
//enum是一種特殊的類型，它是一種值類型，因為每個物品類型都有一個具名常數，而不是使用魔法數字
public enum ItemType
{
    None,
    Meat,
    CookedMeat,
    Wood,
    Campfire
}
//物品定義，這裡寫死
//類，物品類型、物品圖標、物品預置體：將物品的屬性封裝在一個類中，可以更方便地管理和擴展物品的屬性。
//這裡定義了一個構造函數，用於"初始化"物品的屬性>>下面會定義外界獲取物品定義的方法
public class ItemDefine
{
    //ItemType是上方定義的枚舉類型
    public ItemType ItemType;
    //Sprite是Unity中的一種圖片類型，用於表示2D圖片
    public Sprite ItemIcon;
    //GameObject是Unity中的一種遊戲物體類型，用於表示遊戲中的物體
    public GameObject ItemPrefabs;
    //這裡定義了一個構造函數，用於"初始化"物品的屬性>>下面會定義外界獲取物品定義的方法
    public ItemDefine(ItemType itemType, Sprite itemIcon, GameObject itemPrefab)
    {
        ItemType = itemType;
        ItemIcon = itemIcon;
        ItemPrefabs = itemPrefab;
    }
}
/// <summary>
/// 物品管理器
/// </summary>
public class ItemManager : MonoBehaviour
{
    //單例模式
    public static ItemManager instance;
    [SerializeField] Sprite[] itemIcon;
    [SerializeField] GameObject[] itemPrefabs;

    private void Awake()
    {
        instance = this;//實例化
    }
    //獲取物品定義：外界可以根據類型拿到物品配置
    //UI_BagPanel拿來用了
    public ItemDefine GetItemDefine(ItemType itemType)
    {   
        //類型轉換：將枚舉類型轉換為整數類型，然後-1，因為枚舉類型是從1開始的（none不顯示，所以-1）
        return new ItemDefine(itemType, itemIcon[(int)itemType - 1], itemPrefabs[(int)itemType - 1]);
    }
}


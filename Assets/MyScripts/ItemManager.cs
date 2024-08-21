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
public class ItemDefine
{
    public ItemType ItemType;
    public Sprite ItemIcon;
    public GameObject ItemPrefabs;
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
    public ItemDefine GetItemDefine(ItemType itemType)
    {   //沒有none，所以-1
        return new ItemDefine(itemType, itemIcon[(int)itemType - 1], itemPrefabs[(int)itemType - 1]);
    }
}


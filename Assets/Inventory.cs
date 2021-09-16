using LitJson;
using PixelCrushers.DialogueSystem;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInfo
{
    public string name;
    public string displayName;
    public string pickup;
    public float pickupTime;
    public string description;
    public int amount;
    public string dialogue;
    public string variable;
    public string conditionInventory;
    public string animation;
    public bool noItemCollected;
    public bool isImportant;

}
public class AllItemInfo
{
    public List<ItemInfo> allItem;
}
public class Inventory : Singleton<Inventory>
{
    public Dictionary<string, ItemInfo> itemDict = new Dictionary<string, ItemInfo>();

    public int inventoryUnlockedCellCount = 2;
    public string selectedItemName;

    public void addInventoryUnlockedCell()
    {
        inventoryUnlockedCellCount++;

        EventPool.Trigger("updateInventory");
    }
    public bool canAddItem(string itemName, int value = 1)
    {
        GameManager.Instance.player.playSuccessAnim();
        return true;
        //if (itemValueDict.ContainsKey(itemName))
        //{
        //    return true;
        //}
        //if (itemValueDict.Count < inventoryUnlockedCellCount)
        //{
        //    return true;
        //}
        //return false;
    }


    public void updateSelectedItem(string name)
    {
        selectedItemName = name;

        DialogueLua.SetVariable("holdingItem", name);
    }

    public void checkCake(string cakeName)
    {
        bool isvalid = true;
        if (itemDict["wheat"].amount < 2)
        {
            isvalid = false;
        }
        if (itemDict[cakeName].amount < 2)
        {
            isvalid = false;

        }
        DialogueLua.SetVariable("cakeType", cakeName);
        DialogueLua.SetVariable("cakeDoable", isvalid);
        
    }
        public void makeCake()
    {
        consumeItem("wheat", 2);
        string cakeType = DialogueLua.GetVariable("cakeType").asString;
        consumeItem(cakeType, 2);
        addItem(cakeType + "Cake", 1);
    }

    public void sendGift()
    {
        if (selectedItemName == "")
        {
            Debug.LogError("you send what a you send");
            return;
        }
        //sometimes should not be able to send
        consumeItem(selectedItemName, 1);

        selectedItemName = "";

        EventPool.Trigger("updateInventory");
    }

    public void select(string name)
    {
        if(selectedItemName == name)
        {
            updateSelectedItem("");
        }
        else
        {

            updateSelectedItem(name);
        }
        EventPool.Trigger("updateInventory");
    }

    public void addItem(string itemName)
    {
        addItem(itemName, 1);
    }
    public void addItem(string itemName, int value)
    {
        if (!canAddItem(itemName, value))
        {
            Debug.LogError("can't add item " + itemName);
        }
        if (itemDict.ContainsKey(itemName))
        {
            itemDict[itemName].amount += value;
        }
        //else if (itemDict.Count < inventoryUnlockedCellCount)
        //{
        //    itemDict[itemName] = value;
        //}
        EventPool.Trigger("updateInventory");
    }

    public void consumeItem(string itemName, int value = 1)
    {
        if(!itemDict.ContainsKey(itemName) || itemDict[itemName].amount < value)
        {
            Debug.LogError("not enough item to consume");
            return;
        }
        itemDict[itemName].amount -= value;

        //if (itemValueDict[itemName] <= 0)
        //{
        //    itemValueDict.Remove(itemName);
        //}
        EventPool.Trigger("updateInventory");
    }

    public bool hasRake()
    {

        return hasItem("rake");
    }

    public int itemAmount(string itemName)
    {
        return itemDict.ContainsKey(itemName) ? itemDict[itemName].amount : 0;
    }
    public bool hasItemAmount(string itemName,int amount)
    {
        return itemDict.ContainsKey(itemName) && itemDict[itemName].amount >= amount;
    }
    public bool hasItem(string itemName)
    {
        return hasItemAmount(itemName, 1);
    }
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/Item").text;
        var allNPCs = JsonMapper.ToObject<AllItemInfo>(text);
        foreach (ItemInfo info in allNPCs.allItem)
        {
            itemDict[info.name] = info;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach(var key in itemDict.Keys)
            {
                if (!itemDict[key].noItemCollected)
                {
                    itemDict[key].amount += 1;
                }
            }
        }
    }
}

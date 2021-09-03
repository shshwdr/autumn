using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public Dictionary<string, int> itemValueDict = new Dictionary<string, int>();

    public int inventoryUnlockedCellCount = 2;
    public string selectedItemName;
    public bool canAddItem(string itemName, int value = 1)
    {
        if (itemValueDict.ContainsKey(itemName))
        {
            return true;
        }
        if (itemValueDict.Count < inventoryUnlockedCellCount)
        {
            return true;
        }
        return false;
    }
    public void select(string name)
    {
        if(selectedItemName == name)
        {
            selectedItemName = "";
        }
        else
        {

            selectedItemName = name;
        }
        EventPool.Trigger("updateInventory");
    }
    public void addItem(string itemName, int value = 1)
    {
        if (!canAddItem(itemName, value))
        {
            Debug.LogError("can't add item " + itemName);
        }
        if (itemValueDict.ContainsKey(itemName))
        {
            itemValueDict[itemName] += value;
        }
        else if (itemValueDict.Count < inventoryUnlockedCellCount)
        {
            itemValueDict[itemName] = value;
        }
        EventPool.Trigger("updateInventory");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

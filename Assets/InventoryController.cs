using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    List<InventoryCell> inventoryCells = new List<InventoryCell>();
    public void updateInventory()
    {
        int i = 0;
        foreach (var pair in Inventory.Instance.itemDict)
        {
            if (pair.Value.amount > 0)
            {
                inventoryCells[i].gameObject.SetActive(true);
                inventoryCells[i].updateCell(pair.Value);
                i++;
            }
        }
        for(;i< Inventory.Instance.inventoryUnlockedCellCount; i++)
        {
            inventoryCells[i].gameObject.SetActive(false);
            //inventoryCells[i].updateCell(null);
        }
        for (; i < inventoryCells.Count; i++)
        {

            inventoryCells[i].gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            var cell = child.GetComponent<InventoryCell>();
            if (cell)
            {
                inventoryCells.Add(cell);


            }
        }
        updateInventory();
        EventPool.OptIn("updateInventory",updateInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

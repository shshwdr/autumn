using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Image holdItem;
    // Start is called before the first frame update
    void Start()
    {
        holdItem.gameObject.SetActive(false);

        EventPool.OptIn("updateInventory", updateInventory);
    }
    void updateInventory()
    {
        if (Inventory.Instance.selectedItemName != "")
        {
            holdItem.gameObject.SetActive(true);
            holdItem.sprite = Resources.Load<Sprite>("ItemIcon/" + Inventory.Instance.selectedItemName);
        }
        else
        {
            holdItem.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

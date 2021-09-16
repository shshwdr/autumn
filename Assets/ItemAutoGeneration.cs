using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoGeneration : MonoBehaviour
{
    bool isFirstGeneration = true;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("dayChange", generateItems);
    }

    void generateItem(Transform parent)
    {
        var fullName = parent.name.Split('_');
        var prefabName = fullName[0];
        var count = int.Parse( fullName[1]);
        GameObject prefab = Resources.Load<GameObject>("item/" + prefabName);
        var selectedIndex = Utils.randomMultipleIndex(parent.childCount, count);
        if(count == 0 && isFirstGeneration)
        {
            selectedIndex = new List<int>();
            for(int i = 0; i < parent.childCount; i++)
            {
                selectedIndex.Add(i);
            }
        }
        foreach(var selected in selectedIndex)
        {
            var position = parent.GetChild(selected).position;
            if (parent.GetChild(selected).childCount > 0)
            {
                continue;
            }
            Instantiate(prefab, position, Quaternion.identity, parent.GetChild(selected));
        }
    }
    void generateItems()
    {
        foreach(Transform t in transform)
        {
            generateItem(t);
        }
        isFirstGeneration = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

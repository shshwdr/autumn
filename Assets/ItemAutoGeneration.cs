using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoGeneration : MonoBehaviour
{
    public Transform leavesParentTransform;
    public Transform applesParentTransform;
    public int leavesGenerateAmountEachDay = 5;
    public int applesGenerateAmountEachDay = 2;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("dayChange", generateItems);
    }

    void generateItem(Transform parent, int count,string prefabName)
    {
        GameObject prefab = Resources.Load<GameObject>("item/" + prefabName);
        var selectedIndex = Utils.randomMultipleIndex(parent.childCount, count);
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
        generateItem(leavesParentTransform, leavesGenerateAmountEachDay, "leave");
        generateItem(applesParentTransform, applesGenerateAmountEachDay, "carrot");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

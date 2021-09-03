using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    List<Collectable> collectables = new List<Collectable>();
    Collectable lastClosest;
    [HideInInspector]
    public bool isPickingUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickingUp)
        {
            return;
        }
        if (collectables.Count == 0)
        {
            if (lastClosest)
            {
                lastClosest.hidePickupUI();
            }
            return;
        }
        int closestIndex = Utils.findClosestIndex(transform, collectables);
        collectables[closestIndex].showPickupUI();
        if (lastClosest && lastClosest!= collectables[closestIndex])
        {
            lastClosest.hidePickupUI();
        }
        lastClosest = collectables[closestIndex];

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lastClosest)
            {
                StartCoroutine(pickupItem());
            }
        }
    }

    IEnumerator pickupItem()
    {
        isPickingUp = true;
        yield return  lastClosest.startPicking();
        isPickingUp = false;
    }
    public void addCanPickup(Collectable c)
    {
        collectables.Add(c);
    }

    public void removeCanPickup(Collectable c)
    {
        collectables.Remove(c);
    }

}

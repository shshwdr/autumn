using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    List<InteractiveItem> collectables = new List<InteractiveItem>();
    InteractiveItem lastClosest;
    [HideInInspector]
    public bool isPickingUp;
    public GameObject pickingUpBar;
    Animator animator;
    private void Awake()
    {
        pickingUpBar.SetActive(false);
        animator = GetComponentInChildren<Animator>();
    }
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
                GetComponent<PlayerMove>().testFlip(lastClosest.transform.position - transform.position);
                lastClosest.interact(this);
                //lastClosest.startPicking(this);
                //StartCoroutine(pickupItem());
            }
        }
    }



    public void startPickupItem()
    {
        isPickingUp = true;
        animator.SetBool("finding", true);
       // yield return  lastClosest.startPicking(pickingUpBar);
    }

    public void finishPickupItem()
    {

        isPickingUp = false;
        animator.SetBool("finding", false);
    }
    public void addCanPickup(InteractiveItem c)
    {
        collectables.Add(c);
    }

    public void removeCanPickup(InteractiveItem c)
    {
        collectables.Remove(c);
    }

}

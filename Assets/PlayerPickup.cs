using Pool;
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

    CircleCollider2D triggerCollider;
    public float handRange = 0.5f;
    public float rakeRange = 1.5f;

    private void Awake()
    {
        pickingUpBar.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        triggerCollider = GetComponent<CircleCollider2D>();

        EventPool.OptIn("updateInventory",updateRake);
        GameManager.Instance.player = this;
    }

    public void failedPickup()
    {
        animator.SetTrigger("wrong");
    }

    void updateRake()
    {
        if (Inventory.Instance.hasRake())
        {
            triggerCollider.radius = rakeRange;
        }
        else
        {

            triggerCollider.radius = handRange;
        }
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

    public void startFishing()
    {
        
        isPickingUp = true;
        animator.SetTrigger("startFish");
    }
    public void fishBiting()
    {

        animator.SetTrigger("fishBite");
    }
    public void finishFishing()
    {

        animator.SetTrigger("finishFish");
        StartCoroutine(fullyFinishFishing());
    }
    public IEnumerator fullyFinishFishing()
    {
        yield return new WaitForSeconds(0.5f);
        isPickingUp = false;
    }

    public void stopFishing()
    {

        isPickingUp = false;
        animator.SetTrigger("stopFish");
    }

    public void startPlayAnimation(string name)
    {

        isPickingUp = true;
        animator.SetBool(name, true);
    }
    public void finishPlayAnimation(string name)
    {

        isPickingUp = false;
        animator.SetBool(name, false);
    }
    public void startPickupItem()
    {
        isPickingUp = true;
        animator.SetBool("hasRake", Inventory.Instance.hasRake());
        animator.SetBool("finding", true);

        // yield return  lastClosest.startPicking(pickingUpBar);
    }

    public void startUpgrating()
    {
        isPickingUp = true;
        animator.SetBool("upgrade", true);

        // yield return  lastClosest.startPicking(pickingUpBar);
    }

    public void stopUpgrating()
    {
        isPickingUp = false;
        animator.SetBool("upgrade", false);

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

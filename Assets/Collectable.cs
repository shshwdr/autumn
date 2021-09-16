using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : InteractiveItem
{
    public float pickingUpTime;
    public string itemName = "leave";
    public bool shouldHideAtBeginning = false;
    ItemInfo info;

    public Sprite[] randomSprites;
    void Awake()
    {
        QuestManager.Instance.itemsDict[name] = gameObject;
        if (shouldHideAtBeginning)
        {
            gameObject.SetActive(false);
        }
        renderer = GetComponentInChildren<SpriteRenderer>();
        if (randomSprites.Length > 0)
        {
            renderer.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
        }
    }
    public override void Start()
    {
        if (!Inventory.Instance.itemDict.ContainsKey(itemName))
        {
            Debug.Log("do not have key " + itemName);
        }
        info = Inventory.Instance.itemDict[itemName];
        base.Start();
        interactiveText.text = info.pickup;
        pickingUpTime = info.pickupTime;
    }

    
    void showPickingUpBar(GameObject pickingUpBar)
    {
        pickingUpBar.SetActive(true);
        Image pickingUpImage = pickingUpBar.GetComponentsInChildren <Image>()[1];
        pickingUpImage.fillAmount = 0;
        DOTween.To(() => pickingUpImage.fillAmount, x => pickingUpImage.fillAmount = x, 1, pickingUpTime);

    }

    protected override bool canInteract()
    {
        if(info.conditionInventory != null)
        {
            return Inventory.Instance.hasItem(info.conditionInventory);
        }
        return true;
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        player.pickingUpBar.SetActive(true);
        showPickingUpBar(player.pickingUpBar);
        if (info.animation!=null)
        {
            player.startPlayAnimation(info.animation);
        }
        else
        {
            player.startPickupItem();
        }
        StartCoroutine(pickupItem(player));
    }

    IEnumerator pickupItem(PlayerPickup player)
    {

        yield return new WaitForSeconds(pickingUpTime);
        if (info.variable!=null)
        {
            DialogueLua.SetVariable(info.variable, DialogueLua.GetVariable(info.variable).asInt + 1);
        }
        if(!info.noItemCollected)
        {

            Inventory.Instance.addItem(itemName, 1);
        }
        QuestManager.Instance.addQuestItem(itemName, 1);
        //DialogueLua.SetVariable("cleanedLeaves", DialogueLua.GetVariable("cleanedLeaves").asInt + 1);
        if (info.animation != null)
        {
            player.finishPlayAnimation(info.animation);
        }
        else
        {
            player.finishPickupItem();
        }
        player.pickingUpBar.SetActive(false);
        if (info.dialogue!=null)
        {
            DialogueManager.instance.StartConversation(info.dialogue);
        }
        Destroy(gameObject);
    }
}

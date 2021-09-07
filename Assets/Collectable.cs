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
    public override void Start()
    {
        base.Start();
        interactiveText.text = Inventory.Instance.itemDict[itemName].pickup;
        pickingUpTime = Inventory.Instance.itemDict[itemName].pickupTime;
    }

    
    void showPickingUpBar(GameObject pickingUpBar)
    {
        pickingUpBar.SetActive(true);
        Image pickingUpImage = pickingUpBar.GetComponentsInChildren <Image>()[1];
        pickingUpImage.fillAmount = 0;
        DOTween.To(() => pickingUpImage.fillAmount, x => pickingUpImage.fillAmount = x, 1, pickingUpTime);

    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        player.pickingUpBar.SetActive(true);
        showPickingUpBar(player.pickingUpBar);
        player.startPickupItem();
        StartCoroutine(pickupItem(player));
    }

    IEnumerator pickupItem(PlayerPickup player)
    {

        yield return new WaitForSeconds(pickingUpTime);
        Inventory.Instance.addItem(itemName, 1);
        QuestManager.Instance.addQuestItem(itemName, 1);
        //DialogueLua.SetVariable("cleanedLeaves", DialogueLua.GetVariable("cleanedLeaves").asInt + 1);
        player.finishPickupItem();
        player.pickingUpBar.SetActive(false);
        Destroy(gameObject);
    }
}

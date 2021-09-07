using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHouse : InteractiveItem
{
    public string showText = "Require 10 leaves to upgrade";
    bool isUpgrading;
    PlayerPickup player;
    float upgradeTime = 3f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        interactiveText.text = showText;
    }
    public override void interact(PlayerPickup p)
    {
        base.interact(player);
        player = p;
        player.startFishing();
        isUpgrading = true;
        StartCoroutine(upgrade(player));
        //fishWaitTime = Random.Range(fishWaitingTimeMin, fishWaitingTimeMax);
        //currentWaitTime = 0;
        //gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator upgrade(PlayerPickup player)
    {

        yield return new WaitForSeconds(upgradeTime);
        //Inventory.Instance.addItem(itemName, 1);
        //QuestManager.Instance.addQuestItem(itemName, 1);
        //DialogueLua.SetVariable("cleanedLeaves", DialogueLua.GetVariable("cleanedLeaves").asInt + 1);
        player.finishFishing();
        player.pickingUpBar.SetActive(false);
    }
}

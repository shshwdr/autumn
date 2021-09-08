using LitJson;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyValue {
    public string Key;
    public int Value;
}

public class UpgradeInfo
{
    public string name;
    public string displayName;
    public string description;
    public int buildTime;
    public KeyValue[] items;
    public bool isMax;
}
public class AllUpgradeInfo
{
    public List<UpgradeInfo> allUpgrade;
}
public class UpgradeHouse : InteractiveItem
{
    List<UpgradeInfo> updateList;
    public TMP_Text interactiveTitle;

    bool isUpgrading;
    PlayerPickup player;
    float upgradeTime = 3f;
    int currentIndex;
    public override void prepareUI()
    {
        base.prepareUI();
        UpgradeInfo info = updateList[currentIndex];
        interactiveTitle.text = info.displayName;
        if (info.isMax)
        {
            interactiveText.text = info.description;
        }
        else
        {
            string requirement = "";

            foreach (var pair in info.items)
            {
                if (Inventory.Instance.hasItemAmount(pair.Key, pair.Value))
                {
                    requirement += "<color=green>";
                }
                else
                {

                    requirement += "<color=red>";
                }
                requirement += pair.Value.ToString() + " " + Inventory.Instance.itemDict[pair.Key].displayName + " ";

                requirement += "</color>";
            }
            interactiveText.text = string.Format(info.description, requirement);
        }

    }
    private void Awake()
    {

        string text = Resources.Load<TextAsset>("json/houseUpgrade").text;
        var allNPCs = JsonMapper.ToObject<AllUpgradeInfo>(text);
        updateList = allNPCs.allUpgrade;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    bool canUpgrade()
    {
        UpgradeInfo info = updateList[currentIndex];
        if (info.isMax)
        {
            DialogueManager.ShowAlert("My house is perfect!");
            return false;
        }
        foreach (var pair in info.items)
        {
            if (!Inventory.Instance.hasItemAmount(pair.Key, pair.Value))
            {
                DialogueManager.ShowAlert("Not enough resource to upgrade!");
                return false;
            }
        }
        return true;
    }

    void upgradeConsume()
    {
        UpgradeInfo info = updateList[currentIndex];
        foreach (var pair in info.items)
        {
            if (Inventory.Instance.hasItemAmount(pair.Key, pair.Value))
            {
                Inventory.Instance.consumeItem(pair.Key, pair.Value);
            }
        }
    }

    public override void interact(PlayerPickup p)
    {

        player = p;
        if (canUpgrade())
        {

            base.interact(player);
            player.startFishing();
            isUpgrading = true;
            StartCoroutine(upgrade(player));
            //fishWaitTime = Random.Range(fishWaitingTimeMin, fishWaitingTimeMax);
            //currentWaitTime = 0;
            //gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            player.failedPickup();
        }

    }

    IEnumerator upgrade(PlayerPickup player)
    {

        yield return new WaitForSeconds(upgradeTime);
        //Inventory.Instance.addItem(itemName, 1);
        //QuestManager.Instance.addQuestItem(itemName, 1);
        //DialogueLua.SetVariable("cleanedLeaves", DialogueLua.GetVariable("cleanedLeaves").asInt + 1);
        player.finishFishing();
        player.pickingUpBar.SetActive(false);
        upgradeConsume();
        currentIndex++;
        QuestManager.Instance.addQuestItem("houseUpgradeLevel", 1);
        //DialogueLua.SetVariable("houseUpgradeLevel", currentIndex);
    }
}

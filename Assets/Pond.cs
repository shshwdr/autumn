using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : InteractiveItem
{
    public string showText = "Start Fishing";
    bool isFishing;
    PlayerPickup player;

    public float fishWaitingTimeMin = 3f;
    public float fishWaitingTimeMax = 8f;
    float fishWaitTime;
    float currentWaitTime;
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
        isFishing = true;
        fishWaitTime = Random.Range(fishWaitingTimeMin, fishWaitingTimeMax);
        currentWaitTime = 0;
        //gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFishing && Input.GetKeyDown(KeyCode.Space))
        {
            isFishing = false;
            player.finishFishing();
        }
        if (isFishing)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= fishWaitTime)
            {

                isFishing = false;
                player.finishFishing();
                getReward();
            }
        }
    }

    void getReward()
    {

        Inventory.Instance.addItem("fish", 1);
        QuestManager.Instance.addQuestItem("fish", 1);
        DialogueManager.ShowAlert("Caught Fish!");
    }
}

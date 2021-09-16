using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : InteractiveItem
{
    public string showText = "Start Fishing";
    bool isFishing;
    PlayerPickup player;
    bool isFishBiting;
    public float fishWaitingTimeMin = 3f;
    public float fishWaitingTimeMax = 8f;

    public float fishBiteTime = 1f;
    float currentFishBiteTime = 0f;
    float fishWaitTime;
    float currentWaitTime;
    bool fullyFinished = true;
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
            if (isFishBiting)
            {
                finishFish(true);

            }
        }
        if (isFishing)
        {
            if (isFishBiting)
            {
                currentFishBiteTime += Time.deltaTime;
                if (currentFishBiteTime >= fishBiteTime)
                {
                    finishFish(false);
                }
            }
            else
            {

                currentWaitTime += Time.deltaTime;
                if (currentWaitTime >= fishWaitTime)
                {

                    isFishBiting = true;
                    currentFishBiteTime = 0;
                    player.fishBiting();
                }
            }
        }
    }

    void finishFish(bool succeed)
    {
        currentFishBiteTime = 0;
        isFishBiting = false;
        if (succeed)
        {

            player.finishFishing();
            getReward();
        }
        else
        {

            player.stopFishing();
        }
        StartCoroutine(finalFinish());
    }

    IEnumerator finalFinish()
    {
        yield return new WaitForSeconds(0.1f);

        isFishing = false;
    }
    protected override bool canInteract()
    {
        Inventory.Instance.hasItem("fishrod");
        return true;
    }

    void getReward()
    {

        Inventory.Instance.addItem("fish", 1);
        QuestManager.Instance.addQuestItem("fish", 1);
        DialogueManager.ShowAlert("Caught Fish!");
    }
}

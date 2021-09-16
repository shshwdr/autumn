using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public class NPC : MonoBehaviour
{
    [HideInInspector]
    public NPCInfo info;
    //public string name;
    Talkable talkable;
    NPCBehavior currentBehavior;
    NPCPathFinding pathFinding;

    public GameObject willGrantQuestMarker;
    public GameObject canFinishQuestMarker;

    public GameObject renderObjects;

    public bool isActived;

    public bool isVisible = true;

    public void activate()
    {
        isActived = true;
    }

    public void hideAllQuestMarkers()
    {
        willGrantQuestMarker.SetActive(false);
        canFinishQuestMarker.SetActive(false);
    }
    public void willGrantQuest()
    {
        hideAllQuestMarkers();
        willGrantQuestMarker.SetActive(true);

    }
    public void canFinishQuest()
    {
        hideAllQuestMarkers();
        canFinishQuestMarker.SetActive(true);

    }

    protected void Awake()
    {
        talkable = GetComponent<Talkable>();
        pathFinding = GetComponent<NPCPathFinding>();
        hideAllQuestMarkers();
        info = NPCManager.Instance.npcDict[name];
        NPCManager.Instance.npcScriptDict[info.name] = this;
        isActived = info.isActived;

    }
        // Start is called before the first frame update
        void Start()
    {
        if (name != "leader")
        {

            renderObjects.SetActive(false);

            isVisible = false;
        }
        info = NPCManager.Instance.npcDict[name];
        transform.position =  ScenePositionManager.Instance.positionDict[info.initPosition].position;
        EventPool.OptIn("hourChange", HourChanged);
    }



    void HourChanged()
    {
        if (!isActived)
        {
            return;
        }
        DayTime time = DayTimeManager.Instance.gameTime;
        foreach (NPCBehavior behavior in info.behaviors)
        {
            if(behavior.weekdays!=null && !Utils.arrayContains(behavior.weekdays,time.weekday))
            {
                continue;
            }
            if (behavior.ignoreWeekdays != null && Utils.arrayContains(behavior.ignoreWeekdays, time.weekday))
            {
                continue;
            }
            if (behavior.days != null && !Utils.arrayContains(behavior.days, time.day))
            {
                continue;
            }
            if (behavior.ignoreDays != null && Utils.arrayContains(behavior.ignoreDays, time.day))
            {
                continue;
            }
            if(behavior.finishQuest!=null && 
                QuestManager.Instance.questDict[behavior.finishQuest].state != QuestState.success)
            {
                continue;
            }
            if (behavior.time == time.hour)
            {
                StartCoroutine(moveTo(behavior));
            }
        }
    }

    IEnumerator moveTo(NPCBehavior behavior)
    {
        yield return new WaitForSeconds(behavior.delay);
        pathFinding.setTarget(ScenePositionManager.Instance.positionDict[behavior.destination]);
        //move to destination
        currentBehavior = behavior;

        talkable.enableInteractive();
        renderObjects.SetActive(true);

        isVisible = true;
        if (currentBehavior.startDialogue != null)
        {
            DialogueLua.SetActorField(name, "dialog", currentBehavior.startDialogue);
        }
        else
        {

            DialogueLua.SetActorField(name, "dialog", "");
        }
        yield break;
        //rb.DOMove(ScenePositionManager.Instance.positionDict[behavior.destination].position, 1);
        //yield return new WaitForSeconds(1);
        //transform.position = ScenePositionManager.Instance.positionDict[behavior.destination].position;
        //if (behavior.shouldHide)
        //{
        //    spriteObject.SetActive(false);
        //    talkable.disableInteractive();

        //}
    }

    public void finishPath()
    {
        if (currentBehavior == null)
        {
            Debug.LogError("no current behanior");
            return;
        }
        if (currentBehavior.shouldHide)
        {
            renderObjects.SetActive(false);
            isVisible = false;
            //talkable.disableInteractive();

        }
        if (currentBehavior.finishDialogue != null)
        {
            DialogueLua.SetActorField(name, "dialog", currentBehavior.finishDialogue);
        }
        else
        {

            DialogueLua.SetActorField(name, "dialog", "");
        }

        currentBehavior = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC : CharacterMove
{
    [HideInInspector]
    public NPCInfo info;
    public string name;
    bool isActive;
    Talkable talkable;
    NPCBehavior currentBehavior;
    NPCPathFinding pathFinding;

    protected override void Awake()
    {
        base.Awake();
        talkable = GetComponent<Talkable>();
        pathFinding = GetComponent<NPCPathFinding>();
    }
        // Start is called before the first frame update
        void Start()
    {
        spriteObject.SetActive(false);
        info = NPCManager.Instance.npcDict[name];
        transform.position =  ScenePositionManager.Instance.positionDict[info.initPosition].position;
        EventPool.OptIn("hourChange", HourChanged);
    }



    void HourChanged()
    {
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
            if (behavior.time == time.hour)
            {
                StartCoroutine(moveTo(behavior));
            }
        }
    }

    IEnumerator moveTo(NPCBehavior behavior)
    {
        pathFinding.setTarget(ScenePositionManager.Instance.positionDict[behavior.destination]);
        //move to destination
        isActive = true;
        currentBehavior = behavior;

        talkable.enableInteractive();
        spriteObject.SetActive(true);
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
            spriteObject.SetActive(false);
            talkable.disableInteractive();

        }
        currentBehavior = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

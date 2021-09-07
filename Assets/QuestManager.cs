using LitJson;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestReward
{

    public int amount;
    public string type;
    public string subtype;
}
public class QuestEntry
{
    public int amount;
    public string type;
    public string subtype;
    public string text;
    public QuestState state;
}

public class QuestInfo
{
    public string name;
    public string displayName;
    public string returnNPC;
    public string activateNext;
    public QuestState state;
    public QuestReward[] reward;
    public QuestEntry[] entries;
}
public class AllQuestInfo
{
    public List<QuestInfo> allQuest;
}
public enum QuestState { unassigned, active, returnToNPC,success}
public class QuestManager : Singleton<QuestManager>
{

    public QuestController questController;
    public Dictionary<string, QuestInfo> questDict;
    public Dictionary<string, int> questAmountItemDict = new Dictionary<string, int>();

    public void addQuestItem(string name, int amount)
    {
        if(name == "leave")
        {

            if (!questAmountItemDict.ContainsKey(name))
            {
                questAmountItemDict[name] = 0;
            }
            questAmountItemDict[name] += amount;

            updateQuestState();
            questController.UpdateQuest();
        }
    }
    public List<QuestInfo> activeQuests()
    {
        var res = new List<QuestInfo>();
        foreach (var info in QuestManager.Instance.questDict.Values)
        {
            if (info.state == QuestState.active || info.state == QuestState.returnToNPC)
            {
                res.Add(info);
            }
        }
        return res;
    }
    void updateQuestState()
    {
        foreach(var info in activeQuests())
        {
            bool isFinished = true;
            foreach(var entry in info.entries)
            {
                switch (entry.type)
                {
                    case "questItemAmount":
                        if (getQuestItemAmount(entry.subtype) >= entry.amount)
                        {
                            entry.state = QuestState.success;
                        }
                        break;
                }
                if (entry.state != QuestState.success)
                {
                    isFinished = false;
                }
            }
            if (isFinished)
            {
                info.state = QuestState.returnToNPC;

                DialogueLua.SetQuestField(info.name, "State", "returnToNPC");
            }
        }
    }
    public void finishQuest(string name)
    {
        var info = questDict[name];
        info.state = QuestState.success;

        questController.UpdateQuest();
        DialogueLua.SetQuestField(name, "State", "success");

        getReward(info);
        startNextQuest(info);

    }
    void startNextQuest(QuestInfo info)
    {
        if (info.activateNext!=null)
        {
            activateQuest(info.activateNext);
        }
    }

    void getReward(QuestInfo info)
    {
        foreach (var reward in info.reward)
        {
            switch (reward.type)
            {
                case "friendship":

                    DialogueLua.SetActorField(reward.subtype, "friendship", DialogueLua.GetActorField(reward.subtype, "friendship").asInt + reward.amount);
                    DialogueManager.ShowAlert("Friendship increased!");
                    break;
                case "item":
                    Inventory.Instance.addItem(reward.subtype);
                    DialogueManager.ShowAlert("Get a " + reward.subtype+"!");//Inventory.Instance.item);
                    break;
            }

        }
    }

    public int getQuestItemAmount(string name)
    {

        if (!questAmountItemDict.ContainsKey(name))
        {
            return 0;
        }
        return questAmountItemDict[name];
    }
    // Start is called before the first frame update
    void Start()
    {

        string text = Resources.Load<TextAsset>("json/Quest").text;
        var allNPCs = JsonMapper.ToObject<AllQuestInfo>(text);
        questDict = new Dictionary<string, QuestInfo>();
        foreach (QuestInfo info in allNPCs.allQuest)
        {
            questDict[info.name] = info;
        }
        questController.UpdateQuest();
    }

    public void activateQuest(string name)
    {

        questDict[name].state = QuestState.active;
        questController.UpdateQuest();
    }

    public void updateQuestController()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

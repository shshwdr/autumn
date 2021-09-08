using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCell : MonoBehaviour
{
    public TMP_Text title;
    string returnToNPCString = "Talk to {0}";
    List<GameObject> entries = new List<GameObject>();
    // Start is called before the first frame update
    public void FakeAwake()
    {
        foreach (Transform child in transform)
        {
            if(child.name == "title")
            {
                continue;
            }
            entries.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }
    public void updateCell(QuestInfo info)
    {
        title.text = info.displayName;
        int i = 0;
        foreach(var entry in info.entries)
        {
            entries[i].SetActive(true);

            switch (entry.type)
            {
                case "questItemAmount":
                    entries[i].GetComponentInChildren<TMP_Text>().text = string.Format( entry.text,QuestManager.Instance.getQuestItemAmount(entry.subtype), entry.amount);
                    break;
            }
            if(entry.state == QuestState.success)
            {
                entries[i].GetComponentInChildren<TMP_Text>().color = Color.green;
            }
            else
            {
                entries[i].GetComponentInChildren<TMP_Text>().color = Color.white;

            }
            i++;
        }
        if(info.state == QuestState.returnToNPC)
        {

            entries[i].SetActive(true);

            entries[i].GetComponentInChildren<TMP_Text>().text = string.Format(returnToNPCString, NPCManager.Instance.npcDict[ info.returnNPC].displayName);

            i++;
        }
        for (; i < entries.Count; i++)
        {

            entries[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

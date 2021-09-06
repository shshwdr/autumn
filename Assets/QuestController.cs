using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public GameObject questParent;
    List<QuestCell> cells = new List<QuestCell>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform child in questParent.transform)
        {
            QuestCell cell = child.GetComponent<QuestCell>();
            cells.Add(cell);
            cell.FakeAwake();
            cell.gameObject.SetActive(false);
        }
    }

    public void toggleQuestPanel()
    {
        questParent.SetActive(!questParent.active);
    }


    // Update is called once per frame
    public void UpdateQuest()
    {
        int i = 0;
        foreach(var info in QuestManager.Instance.activeQuests())
        {
                QuestCell cell = cells[i];
                cell.updateCell(info);
                cell.gameObject.SetActive(true);
                i++;

        }
        for (; i < cells.Count; i++)
        {

            cells[i].gameObject.SetActive(false);
        }
        //questParent.SetActive(true);
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForEndOfFrame();
        questParent.SetActive(false);
        questParent.SetActive(true);
    }
}

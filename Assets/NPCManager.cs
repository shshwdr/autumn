using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class NPCBehavior {

    public int[] weekdays;
    public int[] days;
    public int[] ignoreDays;
    public int[] ignoreWeekdays;
    public int time;
    public string destination;
    public string dialogue;
    public bool shouldHide;
}

public class NPCInfo
{
    public string name;
    public string displayName;
    public string initPosition;
    public NPCBehavior[] behaviors;
}
public class AllNPCInfo
{
    public List<NPCInfo> allNPC;
}
public class NPCManager : Singleton<NPCManager>
{

    //List<NPCInfo> allNPCs;
    public Dictionary<string, NPCInfo> npcDict;
    private void Awake()
    {
        string text = Resources.Load<TextAsset>("json/NPCBehavior").text;
        //data = JsonMapper.ToObject(text);
        var allNPCs = JsonMapper.ToObject<AllNPCInfo>(text);
        //allNPCs = allNPCs.allNPC;
        npcDict = new Dictionary<string, NPCInfo>();
        foreach (NPCInfo info in allNPCs.allNPC)
        {
            npcDict[info.name] = info;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

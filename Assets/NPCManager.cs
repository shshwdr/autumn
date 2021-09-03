using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class NPCBehavior {
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
public class NPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class NPCBehavior
{
    public int id;
    public string name;
    public int px;
    public int py;
    public int[] collectable;
    public int[] monsters;
    public int[] maxMonsterNumber;
    public bool isDestination;


    public Vector2 position { get { return new Vector2(px, py); } }
    //List<>
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

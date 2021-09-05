using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUtils : Singleton<DialogueUtils>
{
    PlayerPickup playerPick;
    public bool isInDialogue;
    public List<GameObject> hideItems;
    private void Awake()
    {
        playerPick = GameObject.FindObjectOfType<PlayerPickup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startConversation()
    {
        playerPick.isPickingUp = true;
        isInDialogue = true;
        foreach(var item in hideItems)
        {
            item.SetActive(false);
        }
    }

    public void endConversation()
    {
        playerPick.isPickingUp = false;
        isInDialogue = false;
        foreach (var item in hideItems)
        {
            item.SetActive(true);
        }
    }
}

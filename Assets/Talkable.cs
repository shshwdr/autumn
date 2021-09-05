using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : InteractiveItem
{
    NPC npc;
    public override void Start()
    {
        base.Start();
        npc = GetComponent<NPC>();
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    public override void prepareUI() {
        interactiveText.text = "Talk to " + npc.info.displayName;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

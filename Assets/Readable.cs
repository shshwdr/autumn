using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readable : InteractiveItem
{
    public string showText;
    public bool canShow = true;

    public void HideReadable()
    {
        canShow = false;
    }
    protected override bool canShowInteractUI()
    {
        return canShow;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        interactiveText.text = showText;
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }
}

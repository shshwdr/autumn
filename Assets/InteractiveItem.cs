using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveItem:MonoBehaviour
{
    public GameObject pickUI;
    public TMP_Text interactiveText;
    public bool isInteractiveDisabled;
    PlayerPickup playerPickup;
    protected SpriteRenderer renderer;
    //public GameObject pickingUpBar;
    // Start is called before the first frame update
    public virtual void Start()
    {
        hidePickupUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canInteract())
        {
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        var player = collision.GetComponent<PlayerPickup>();

        if (player)
        {
            playerPickup = player;
            player.addCanPickup(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canInteract())
        {
            return;
        }
        var player = collision.GetComponent<PlayerPickup>();
        if (player)
        {
            player.removeCanPickup(this);
        }
    }

    protected virtual bool canInteract()
    {
        return true;
    }

    public virtual void interact(PlayerPickup player)
    {

        if (!canInteract())
        {
            player.failedPickup();
            DialogueManager.ShowAlert("Need extra equipment!");
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        pickUI.SetActive(false);


    }
    public virtual void prepareUI() { }
    public void showPickupUI()
    {

        if (!canInteract())
        {
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        prepareUI();
        //show pick up
        pickUI.SetActive(true);
    }
    public void hidePickupUI()
    {

        //pickingUpBar.SetActive(false);
        //show pick up
        pickUI.SetActive(false);
    }

    public void disableInteractive()
    {
        isInteractiveDisabled = true;
        if (playerPickup)
        {
            playerPickup.removeCanPickup(this);
        }
        hidePickupUI();
    }

    public void enableInteractive()
    {
        isInteractiveDisabled = false;
    }
}

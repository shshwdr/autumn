using DG.Tweening;
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

        var player = collision.GetComponent<PlayerPickup>();
        if (player)
        {
            player.removeCanPickup(this);
        }
    }


    public virtual void interact(PlayerPickup player)
    {
        if (isInteractiveDisabled)
        {
            return;
        }
        pickUI.SetActive(false);


    }
    public virtual void prepareUI() { }
    public void showPickupUI()
    {

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
        playerPickup.removeCanPickup(this);
        hidePickupUI();
    }

    public void enableInteractive()
    {
        isInteractiveDisabled = true;
    }
}

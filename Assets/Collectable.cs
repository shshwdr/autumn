using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : InteractiveItem
{
    public float pickingUpTime = 0.5f;
    public override void Start()
    {
        base.Start();
        interactiveText.text = "Clean Leaves";
    }
    void showPickingUpBar(GameObject pickingUpBar)
    {
        pickingUpBar.SetActive(true);
        Image pickingUpImage = pickingUpBar.GetComponentInChildren<Image>();
        pickingUpImage.fillAmount = 0;
        DOTween.To(() => pickingUpImage.fillAmount, x => pickingUpImage.fillAmount = x, 1, pickingUpTime);

    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        player.pickingUpBar.SetActive(true);
        showPickingUpBar(player.pickingUpBar);
        player.startPickupItem();
        StartCoroutine(pickupItem(player));
    }

    IEnumerator pickupItem(PlayerPickup player)
    {

        yield return new WaitForSeconds(pickingUpTime);
        Inventory.Instance.addItem("leave", 1);
        player.finishPickupItem();
        player.pickingUpBar.SetActive(false);
        Destroy(gameObject);
    }
}

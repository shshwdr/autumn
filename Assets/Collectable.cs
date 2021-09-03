using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public GameObject pickUI;
    public GameObject pickingUpBar;
    public float pickingUpTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        hidePickupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerPickup>();
        if (player)
        {
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


    void showPickingUpBar()
    {
        Image pickingUpImage = pickingUpBar.GetComponentInChildren<Image>();
        pickingUpImage.fillAmount = 0;
        DOTween.To(() => pickingUpImage.fillAmount, x => pickingUpImage.fillAmount = x, 1, pickingUpTime);

    }
    public IEnumerator startPicking()
    {
        pickUI.SetActive(false);
        pickingUpBar.SetActive(true);
        showPickingUpBar();
        yield return new WaitForSeconds(pickingUpTime);
        Inventory.Instance.addItem("leave", 1);
        Destroy(gameObject);
    }
    public void showPickupUI()
    {

        //show pick up
        pickUI.SetActive(true);
    }
    public void hidePickupUI()
    {

        pickingUpBar.SetActive(false);
        //show pick up
        pickUI.SetActive(false);
    }
}

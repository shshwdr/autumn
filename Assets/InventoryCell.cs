using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler,IPointerClickHandler
{
    public Image image;
    public Image selectImage;
    public TMP_Text valueText;
    public GameObject explainPanel;
    public TMP_Text explainText;
    string name;
    public void updateCell(string n, int value)
    {
        name = n;
        if(value == 0)
        {
            image.gameObject.SetActive(false);
            valueText.gameObject.SetActive(false);

        }
        else
        {

            image.gameObject.SetActive(true);
            valueText.gameObject.SetActive(true);
            valueText.text = value.ToString();
            image.sprite = Resources.Load<Sprite>("ItemIcon/" + name);
            explainText.text = TextUtils.itemNameToExplaination[name];
        }
        if(Inventory.Instance.selectedItemName!=""&& Inventory.Instance.selectedItemName == n)
        {

            selectImage.gameObject.SetActive(true);
        }
        else
        {

            selectImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (name != "")
        {

            explainPanel.SetActive(true);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (name != "")
        {
            explainPanel.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.select(name);
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

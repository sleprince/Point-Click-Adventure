using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    private Item thisItem;
    private Button button;
    private TMPro.TextMeshProUGUI amountText;
    private InventorySystemUI invSystem; //parent class that handles redrawing changes in items held

    [SerializeField] private Image itemImage; //the image that will be attached to the button

    public void Init(Item item, InventorySystemUI invSystem)
    {
        thisItem = item;
        this.invSystem = invSystem; //have to use this. because the internal variable has the same name

        button = GetComponent<Button>();
        amountText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        itemImage.sprite = item.ItemSprite;
        
        amountText.gameObject.SetActive(item.AllowMultiple);
        amountText.text = "x " + thisItem.Amount;
    }

}//class end

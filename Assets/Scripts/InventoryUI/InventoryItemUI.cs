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

    private static Item chosenItem; //has to be static for ItemAction to access it.

    //[SerializeField] private Texture2D itemCursor; //the cursor that the button will change the mouse to.
    [SerializeField] private Image itemImage; //the image that will be attached to the button
    PlayerScript _pScript;
    
    public static Item ChosenItem { get { return chosenItem; } }

    public void Init(Item item, InventorySystemUI invSystem)
    {
        _pScript = PlayerScript.FindObjectOfType<PlayerScript>();
        
        thisItem = item;
        this.invSystem = invSystem; //have to use this. because the internal variable has the same name

        button = GetComponent<Button>();
        amountText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        itemImage.sprite = item.ItemSprite;
        
        amountText.gameObject.SetActive(item.AllowMultiple);
        amountText.text = "x " + thisItem.Amount;
        
        //add listener for each button here.
        //for (int i = 0; i < options.Count; i++)
        //{
            
            //button = options[i];

            //int iD = -1; //fixed this issue, needs to be local variable to work otherwise it kept being 2.
            //iD++;

            //localBtn.name = localBtn.name + i; //to make an id number for each button
            
            button.onClick.AddListener(() => OnClick(button, item));
            
        //}
    }

    void OnClick(Button btn, Item itm)
    {
       Cursor.SetCursor(itm.ItemCursor,_pScript.hotSpot, _pScript.cursorMode); //set the cursor to be the item image.
       GameObject.Find("Inventory UI Panel").gameObject.SetActive(!gameObject.activeInHierarchy); //turn off UI panel.
       chosenItem = itm;
       //set cursor to item mode integer
       _pScript.I = 2;
    }

}//class end

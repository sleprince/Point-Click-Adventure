using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : Actions //used to give and receive items during runtime.
{
    [SerializeField] ItemDatabase itemDatabase; //drag and drop item database object here in editor.
    [SerializeField] bool giveItem; //this will decide whether we are giving or receiving the item.
    [SerializeField] Actions[] yesActions, noActions;
    [SerializeField] string requiredItem;

    private PlayerScript _pScript;

    private InventoryItemUI inventoryUI;

    public int itemId;
    private bool rightItem;

    [SerializeField] Item currentItem; //this solves the null exception reference bug.

    public Item CurrentItem { get { return currentItem; } } //currently chosen item, chosen with ChangeItem function. Is private but made public so
    //can use, but cannot change. Now returns this script's currentItem.

    public ItemDatabase ItemDatabase { get { return itemDatabase; } }

    public void ChangeItem(Item item)
    {
        if (CurrentItem.ItemId == item.ItemId)
            return; //skip code below if the current item is already the correct item.

        if (itemDatabase != null)
            currentItem = Extensions.CopyItem(item);
    }

    public bool ItemMatch (string item)
    {
        bool val = false;
        val = (requiredItem == item);
        return val;
    }

    public override void Act()
    {
         
        
        inventoryUI = FindObjectOfType<InventoryItemUI>();

        Item currItem = InventoryItemUI.ChosenItem;
        
        if(giveItem && currItem != null)
            rightItem = ItemMatch(currItem.ItemName);


            //check if giveItem is true, then give the item
        if (giveItem && rightItem) //only do this action if the currentItem is the requiredItem.
        {
            int itemOwned = DataManager.Instance.Inventory.CheckAmount(CurrentItem);

            //check if we own the item
            if (itemOwned == 1)
            {

                    //pass the item, invoke yesActions
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, -1, true);

                    Extensions.RunActions(yesActions);
                

            }
        }
        else
        {

                    Extensions.RunActions(noActions);
                
                if (!giveItem) //had to add second part otherwise it gives the item when you're trying to use an item
                {
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, 1);
                    Extensions.RunActions(yesActions);
                }
            
        }


        //_pScript = FindObjectOfType<PlayerScript>();


    }

}//class end.


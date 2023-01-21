﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : Actions //used to give and receive items during runtime.
{
    [SerializeField] ItemDatabase itemDatabase; //drag and drop item database object here in editor.
    [SerializeField] bool giveItem; //this will decide whether we are giving or receiving the item.
    [SerializeField] int amount;
    [SerializeField] Actions[] yesActions, noActions;

    public int itemId;

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

    public override void Act()
    {
        //check if giveItem is true, then give the item
        if (giveItem)
        {
            int itemOwned = DataManager.Instance.Inventory.CheckAmount(CurrentItem);

            //check if we own the item
            if (itemOwned > 0)
            {
                if (CurrentItem.AllowMultiple && amount <= itemOwned)
                {
                    //pass the item, invoke yesActions
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, amount, true);

                    Extensions.RunActions(yesActions);
                }
                else if (!CurrentItem.AllowMultiple && itemOwned == 1)
                {
                    //remove the item from inventory, and then invoke yesActions
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, -itemOwned, true);

                    Extensions.RunActions(yesActions);
                }
                else
                {
                    //don't have the item
                    Extensions.RunActions(noActions);
                }
            }
        }
        else
        {
            //else receive the item
            if (CurrentItem.AllowMultiple)
            {
                DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, amount);
                Extensions.RunActions(yesActions);
            }
            else if (!CurrentItem.AllowMultiple)
            {
                if (DataManager.Instance.Inventory.CheckAmount(CurrentItem) == 1)
                {
                    //already have
                    Extensions.RunActions(noActions);
                }
                else
                {
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, 1);
                    Extensions.RunActions(yesActions);
                }
            }
        }
    }

}//class end.


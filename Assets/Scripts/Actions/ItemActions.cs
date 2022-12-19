using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActions : Actions //used to give and receive items during runtime.
{
    [SerializeField] ItemDatabase itemDatabase; //drag and drop item database object here in editor.
    [SerializeField] bool giveItem; //this will decide whether we are giving or receiving the item.
    [SerializeField] Actions[] yesActions, noActions;

    private Item currentItem;

    public override void Act()
    {
		//check if giveItem is true, then give the item
		
             //check if we own the item
 
                    //check if the item has the allowMultiple option or not
 
						//check how many items needed, give and substract our item

                    //else give and remove the item
 
            //else receive the item
    }   
}

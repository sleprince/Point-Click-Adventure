using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParents; //for the item layout.
    [SerializeField] private InventorySystemUI itemUIprefabs;
    [SerializeField] private Inventory playerInventory;

    private List<InventoryItemUI> itemUICollection = new List<InventoryItemUI>(); //all itemUI objects that we instantiate
                                                                                  //at runtime will be stored in this list.
    // Start is called before the first frame update.
    void Start()
    {
        gameObject.SetActive(false); //to make the inventory invisible when the game starts, as this script will be attached to the inventory UI.
        
    }

    void Init(List<Item> items) //initialize
    {
        for (int i = 0; i < items.Count; i++)
        {
            
        }
        
    }


    
} //class end.

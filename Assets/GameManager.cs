using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Inventory plInv;
    [SerializeField] ItemDatabase itemDatabase;

    // Start is called before the first frame update
    void Start()
    {
        //clears the player's inventory and then adds the default items when starting a new game

        plInv.ClearInventory();

        plInv.AddItem(itemDatabase.GetItem(1));
        plInv.AddItem(itemDatabase.GetItem(3));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

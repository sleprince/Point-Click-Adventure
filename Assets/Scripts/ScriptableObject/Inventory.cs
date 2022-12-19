using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Custom Data/Inventory Data")]
public class Inventory : ScriptableObject
{
    [SerializeField] ItemDatabase itemDatabase; //need the itemdatabase data to create each inventory entry.
    [SerializeField] List<Item> inventory = new List<Item>();
}

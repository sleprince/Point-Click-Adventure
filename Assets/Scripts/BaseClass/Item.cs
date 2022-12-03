using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] int itemId;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] bool allowMultiple;
    [SerializeField] int amount;
}

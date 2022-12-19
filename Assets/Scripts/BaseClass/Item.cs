using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] int itemId;
    [SerializeField] string itemName;

    [TextArea(1, 3)]
    [SerializeField] string itemDescription;

    [SerializeField] Sprite itemSprite;
    [SerializeField] bool allowMultiple;
    [SerializeField] int amount;

    //constructor.
    public Item(int itemId, string name, string desc, Sprite sprite, bool allowMultiple)
    {
        this.itemId = itemId; //this. needed because it has the same name.
        itemName = name;
        itemDescription = desc;
        itemSprite = sprite;
        this.allowMultiple = allowMultiple;
    }

    public int ItemId { get { return itemId; } } //public getter, to get the private serializedfields defined above.
    //capital I to differentiate that it is a property and the lowercase i is a variable.
    public string ItemName { get { return itemName; } }
    public string ItemDesc { get { return itemDescription; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public bool AllowMultiple { get { return allowMultiple; } }
    public int Amount { get { return amount; } }

    public void ModifyAmount(int value)
    {
        amount += value;
    }

}//class end

using UnityEngine;

[System.Serializable]
public class Item
{
    public string Slug;
    public string ItemType;
    public GameObject ItemPrefab;
    public int ItemID;

    public string ItemName;
    public string ItemDescription;

    //public string ClothingType; //work, combat, social, naked 
    public bool Stackable;
    public Sprite ItemIcon;

    //constructor for facial hair, hair, weapons, and armor
    public Item(int itemID, string itemName, string itemDescription, string slug, string itemType, GameObject itemPrefab)
    {
        this.ItemID = itemID;
        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;
        this.ItemType = itemType;
        this.ItemPrefab = itemPrefab;
    }
    //constructor for no equipment
    public Item(int itemID, string itemName, string itemDescription, string slug, string itemType)
    {
        this.ItemID = itemID;
        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;
        this.ItemType = itemType;
    }
    //empty constructor
    public Item()
    {
        this.ItemID = -1;
    }

}
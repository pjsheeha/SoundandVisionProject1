using UnityEngine;
using System.Collections.Generic;

public class Equipment : MonoBehaviour
{
    #region Fields
    //gameObjects
    public GameObject avatar;

    public GameObject wornLegs;
    public GameObject wornChest;
    public GameObject wornHair;
    public GameObject wornBeard;
    public GameObject wornMustache;
    public GameObject wornHandRight;
    public GameObject wornChestArmor;

    //lists
    public List<Item> equippedItems = new List<Item>(); 
    //scripts
    private Stitcher stitcher;
    //ints
    private int totalEquipmentSlots; 
    #endregion

    #region Monobehaviour
    public void Awake()
    {
        stitcher = new Stitcher();
    }
    
    public void InitializeEquipptedItemsList()
    {
        totalEquipmentSlots = 8;

        for (int i = 0; i < totalEquipmentSlots; i++)
        {
            equippedItems.Add(new Item());
        }

        AddEquipmentToList(0); //Legs
        AddEquipmentToList(1); //Chest
        AddEquipmentToList(2); //Hair 
        AddEquipmentToList(3); //Beard 
        AddEquipmentToList(4); //Mustache
        AddEquipmentToList(5); //HandRight
        AddEquipmentToList(6); //ChestArmor
        AddEquipmentToList(7); //Feet
    }

    public void AddEquipmentToList(int id)
    {
        for(int i = 0; i < equippedItems.Count; i++)
        {
            if(equippedItems[id].ItemID == -1)
            {
                equippedItems[id] = ItemDatabase.instance.FetchItemByID(id);
                break; 
            }
        }
    }

    public void AddEquipment(Item equipmentToAdd)
    {
        if (equipmentToAdd.ItemType == "Legs")
            wornLegs = AddEquipmentHelper(wornLegs, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "Chest")
            wornChest = AddEquipmentHelper(wornChest, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "Hair")
            wornHair = AddEquipmentHelper(wornHair, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "Beard")
            wornBeard = AddEquipmentHelper(wornBeard, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "Mustache")
            wornMustache = AddEquipmentHelper(wornMustache, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "HandRight")
            wornHandRight = AddEquipmentHelper(wornHandRight, equipmentToAdd);
        else if (equipmentToAdd.ItemType == "ChestArmor")
            wornChestArmor = AddEquipmentHelper(wornChestArmor, equipmentToAdd);
    }

    public GameObject AddEquipmentHelper(GameObject wornItem, Item itemToAddToWornItem)
    {
        wornItem = Wear(itemToAddToWornItem.ItemPrefab, wornItem);
        wornItem.name = itemToAddToWornItem.Slug;
        return wornItem; 
    }

    public void RemoveEquipment(Item equipmentToAdd)
    {
        if (equipmentToAdd.ItemType == "Legs")
            wornLegs = RemoveEquipmentHelper(wornLegs, 0);
        else if (equipmentToAdd.ItemType == "Chest")
            wornChest = RemoveEquipmentHelper(wornChest, 1);
        else if (equipmentToAdd.ItemType == "Hair")
            wornHair = RemoveEquipmentHelper(wornHair, 2);
        else if (equipmentToAdd.ItemType == "Beard")
            wornBeard = RemoveEquipmentHelper(wornBeard, 3);
        else if (equipmentToAdd.ItemType == "Mustache")
            wornMustache = RemoveEquipmentHelper(wornMustache, 4);
        else if (equipmentToAdd.ItemType == "HandRight")
            wornHandRight = RemoveEquipmentHelper(wornHandRight, 5);
        else if (equipmentToAdd.ItemType == "ChestArmor")
            wornChestArmor = RemoveEquipmentHelper(wornChestArmor, 6);
    }

    public GameObject RemoveEquipmentHelper(GameObject wornItem, int nakedItemIndex)
    {
        wornItem = RemoveWorn(wornItem);
        equippedItems[nakedItemIndex] = ItemDatabase.instance.FetchItemByID(nakedItemIndex);
        return wornItem; 
    }

    #endregion

    private GameObject RemoveWorn(GameObject wornClothing)
    {
        if (wornClothing == null)
            return null;
        GameObject.Destroy(wornClothing);
        return null; 
    }

    private GameObject Wear(GameObject clothing, GameObject wornClothing)
    {
        if (clothing == null)
            return null;
        clothing = (GameObject)GameObject.Instantiate(clothing);
        wornClothing = stitcher.Stitch(clothing, avatar);
        GameObject.Destroy(clothing);
        return wornClothing;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour, IPointerClickHandler
{
    private GameObject unit;
    private ChangeGear changeGearScript;
    private Equipment equipmentScript;
    private Text textChild; 

    private void Start()
    {
        unit = GameObject.FindGameObjectWithTag("Unit").gameObject;
        changeGearScript = unit.GetComponent<ChangeGear>();
        equipmentScript = unit.GetComponent<Equipment>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.name == "Pants")
            AddOrRemoveClothes("naked_legs", "Legs", "pants", 0);
        else if (gameObject.name == "Gambeson")
            AddOrRemoveClothes("naked_chest", "Chest", "gambeson", 1);
        else if (gameObject.name == "Hair")
            AddOrRemoveClothes("bald_head", "Hair", "long_hair", 2);
        else if (gameObject.name == "Beard")
            AddOrRemoveClothes("no_beard", "Beard", "beard", 3);
        else if (gameObject.name == "Mustache")
            AddOrRemoveClothes("no_mustache", "Mustache", "mustache", 4);      
        else if (gameObject.name == "Halberd")
            AddOrRemoveClothes("empty_hand_r", "HandRight", "halberd", 5);
        else if (gameObject.name == "Cuirass")
            AddOrRemoveClothes("no_armor", "ChestArmor", "cuirass", 6);
    }

    public void AddOrRemoveClothes(string nakedSlug, string clothesType, string clothesSlug, int equippedItemsIndex)
    {
        if (equipmentScript.equippedItems[equippedItemsIndex].Slug == nakedSlug)
        {
            changeGearScript.EquipItem(clothesType, clothesSlug);
        }
        else if (equipmentScript.equippedItems[equippedItemsIndex].Slug == clothesSlug)
        {
            changeGearScript.UnequipItem(clothesType, clothesSlug);
        }
    }
}

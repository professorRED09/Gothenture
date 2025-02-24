using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    private PlayerHealth player;
    private PlayerCombat playerMana;
    public int buttonID;
    [SerializeField] Items item;
    public TMP_Text itemDes;
    public TMP_Text itemName;

    //public GameObject statusLog;
    //public TMP_Text statusText;
    //string statusTextTemp;
    private void Start()
    {        
        player = FindObjectOfType<PlayerHealth>();
        playerMana = FindObjectOfType<PlayerCombat>();
    }

    //private Items GetThisItem()
    //{
    //    for(int i=0; i<Inventory.instance.items.Count; i++)
    //    {
    //        if(i == buttonID && Inventory.instance.items[i] != null)
    //        {                
    //            item = Inventory.instance.items[i];
    //        }
    //        else
    //        {

    //        }
    //    }
    //    return item;
    //}

    // return a selected item;
    private Items GetThisItem()
    {
        // return null if player select the slot which doesn't have an item
        if (buttonID >= Inventory.instance.items.Count || Inventory.instance.items[buttonID] == null)
            return item = null;

        item = Inventory.instance.items[buttonID];               
        return item;
    }

    // this function is attached to Use Button
    //public void UseItem()
    //{

    //    //prevent a scenario where player haven't selected any item yet but try to use an item
    //    if (Inventory.instance.selectedItem == null)
    //    {
    //        print("YOU HAVEN'T SELECTED ANY ITEM YET!");
    //        return;
    //    }


    //    //check if the item player tries to use is useable or not
    //    if (Inventory.instance.selectedItem.isUseable == false)
    //    {
    //        print("THIS ITEM CAN'T BE USED YET");
    //        return;
    //    }

    //    //add condition for each item
    //    if (Inventory.instance.selectedItem.name == "Health Potion")
    //    {
    //        player.Heal(10);
    //    }

    //    if (Inventory.instance.selectedItem.name == "Mana Potion")
    //    {
    //        playerMana.RestoreSP(2);
    //    }

    //    //remove an used item from inventory
    //    Inventory.instance.RemoveItem(Inventory.instance.selectedItem);

    //    if (Inventory.instance.selectedItem != null)
    //    {
    //        print("USE " + Inventory.instance.selectedItem.itemName);
    //    }
    //}

    // this function is attached to Use Button
    public void UseItem()
    {
        //prevent a scenario where player haven't selected any item yet but try to use an item
        if (Inventory.instance.selectedItem == null)
        {
            print("YOU HAVEN'T SELECTED ANY ITEM YET!");
            return;
        }

        // apply the item effect & subtract them from the inventory 
        switch (Inventory.instance.selectedItem.name)
        {
            case ("Health Potion"):
                player.Heal(10);                
                Inventory.instance.RemoveItem(Inventory.instance.selectedItem); //remove an used item from inventory
                return;
            case ("Mana Potion"):
                playerMana.RestoreSP(2);
                Inventory.instance.RemoveItem(Inventory.instance.selectedItem); //remove an used item from inventory
                return;
            default:
                print("NEW ITEM, ADD MORE ITEM CASE");
                return;
        }        
    }

    // this function is attached to each item slots
    public void SelectButton()
    {        
        if (GetThisItem() == null)
        {
            Inventory.instance.selectedItem = null;
            //print("Select a item to see a description");
            itemName.text = "---";
            itemDes.text = "Select a item to see a description";
        }
        else
        {
            Inventory.instance.selectedItem = GetThisItem();
            itemName.text = Inventory.instance.selectedItem.itemName;
            itemDes.text = Inventory.instance.selectedItem.itemDes;
            //print("SELECT: " + Inventory.instance.selectedItem.itemName);          
        }
    }

    void resetText()
    {
        itemName.text = "---";
        itemDes.text = "Select a item to see a description";
    }

    //IEnumerator ShowStatus()
    //{
    //    statusLog.SetActive(true);
    //    statusText.text = statusTextTemp;
    //    yield return new WaitForSeconds(2f);
    //    statusLog.SetActive(false);
    //}

}

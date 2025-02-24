using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Inventory : Subject
{    
    public List<Items> items = new List<Items>();   // store all items that player has picked up
    public List<int> itemNumbers = new List<int>(); // store quantity of each items in the inventory
    public GameObject[] slots;                      // reference for item slots in the scene

    public static Inventory instance;
    public Items selectedItem;

    void DisplayItems()
    {
        // looping to show item icons when players open the inventory
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
            slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemNumbers[i].ToString();            
        }
        // looping again to check if some items are still left or not, if there is an item that is already used up, remove it from a slot 
        for (int i = 0; i < slots.Length; i++)
        {
            
            if (i < items.Count)
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);     // turn alpha to 1
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);  // turn alpha to 1
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemNumbers[i].ToString();                
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);     // turn alpha to 0
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;                     
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);  // turn alpha to 0
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;                
            }
        }
    }

    // add an item to the inventory
    public void AddItem(Items _item)
    {
        // if players haven't picked this item before, then add it at 1 unit
        if (!items.Contains(_item))
        {
            items.Add(_item);
            itemNumbers.Add(1);
        }
        else
        {
            Debug.Log("You already have this one");
            // looping through the items list to find this item and add it + 1
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == _item)
                {
                    itemNumbers[i]++;
                }
            }
        }
        DisplayItems(); 
        NotifyObserver(PlayerAction.Pick); 
    }

    // remove an item from the inventory
    public void RemoveItem(Items _item)
    {
        // check if players have this item before or not
        if (items.Contains(_item))
        {
            // looping through the items list to find this item and minus it 1 unit
            for (int i=0; i<items.Count; i++)
            {
                if(_item == items[i])
                {
                    itemNumbers[i]--;

                    // if this item is already out, then remove it from the array
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(_item);
                        itemNumbers.Remove(itemNumbers[i]);
                        selectedItem = null;
                    }
                }                
            }
        }
        else
        {
            selectedItem = null;
            print("You got nothing in this slot");
        }
        DisplayItems();
    }

    public bool HasItem(string itemName)
    {
        return items.Any(item => item.itemName == itemName);
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayItems();
    }

    
    
}

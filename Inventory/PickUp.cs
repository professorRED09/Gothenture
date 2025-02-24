using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Items itemData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            Destroy(gameObject);            

            Inventory.instance.AddItem(itemData);
        }
    }
}

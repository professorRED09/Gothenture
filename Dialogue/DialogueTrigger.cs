using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Subject
{
    [Header("Reference")]
    public GameObject eButtonPrompt;
    private BoxCollider2D col;    

    [Header("Dialogues")]
    public Dialogue dialogueT; // for player who have collected all require items
    public Dialogue dialogueF; // for player who have not collected all require items yet

    [Header("Setting")]
    [SerializeField] bool needToCheckItem; // checked if the dialogue need to check player's items
    [SerializeField] bool invokeEventLater; // checked if the dialogue will invoke an event after

    [SerializeField] Items[] itemList;

    int itemCount;


    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void TriggerDialogue()
    {
        if (needToCheckItem) {
            CheckItemsOnPlayer();
        }
        else {
            print("TRIGGER DIALOGUE");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueT);
        }        
    }

    private void CheckItemsOnPlayer()
    {
        itemCount = 0; // reset value everytime player interact with

        for(int i = 0; i<itemList.Length; i++)
        {
            if (Inventory.instance.HasItem(itemList[i].itemName))
            {
                itemCount++;
                print(itemCount);
            }
        }
        
        if (itemCount == itemList.Length) {
            print("TRIGGER DIALOGUE");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueT);

            if (invokeEventLater) {
                NotifyObserver(PlayerAction.Event);
                //InvokeEvent();
            }
            
        }
        else {
            print("TRIGGER DIALOGUE");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueF);
        }        
    } 
   

    private void OnTriggerEnter2D(Collider2D other)
    {        
        
        if (other.CompareTag("Player"))
        {
            
            eButtonPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eButtonPrompt.SetActive(false);
        }
    }


}

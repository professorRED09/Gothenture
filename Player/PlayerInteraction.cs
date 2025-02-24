using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Subject
{
    [Header("Ref")]
    public PlayerHealth player;
    [SerializeField] GameObject inventory;
    //[SerializeField] GameObject pauseMenu;
    [SerializeField] bool isToggleOn;

    [Header("Setting")]
    public float radius;
    public LayerMask interactLayers;
    //private Collider2D[] hits;
    //private KeyCode keyCode;   

    private void Start()
    {
        inventory.SetActive(false); // hide inventory panel early
        //pauseMenu.SetActive(false); // hide pause menu early
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead) // if player is dead, then no need to check for an input
            return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    ShowPauseMenu();
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            isToggleOn = !isToggleOn; // make it into an opposite value
            ShowInventory();
        }
    }

    //void Resume() { 
    //}
    //void ShowPauseMenu()
    //{
    //    pauseMenu.SetActive(true);
    //    NotifyObserver(PlayerAction.Pause);
    //}

    //This function will show/hide inventory panel and also pause the game based on isToggleOn
    void ShowInventory()
    {
        if (isToggleOn)
        {
            inventory.SetActive(true);
            NotifyObserver(PlayerAction.Pause);
            //GameManager.instance.PauseGame();
        }
        else
        {
            inventory.SetActive(false);
            NotifyObserver(PlayerAction.Resume);
            //GameManager.instance.ResumeGame();
        }
    }

    //Checking interact radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // This function handle all interaction; do dialogue, open chest
    void HandleInteraction()
    {
        print("E Detect");        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, interactLayers);
        if (hit == null) 
        {
            print("DETECT NOTHING");
            return;
        }
            
        // if detect gameObject with dialogue, then trigger it 
        if (hit.GetComponent<DialogueTrigger>() != null)
        {
            print("TALK");
            hit.GetComponent<DialogueTrigger>().TriggerDialogue();
            NotifyObserver(PlayerAction.Talk);
            
        }
        // if detect chest, then open it 
        else if (hit.GetComponent<Chest>() != null)
        {
            print("OPEN CHEST");
            hit.GetComponent<Chest>().OpenChest();
        }
    }
}

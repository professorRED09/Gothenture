using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Reference")]
    public GameObject eButtonPrompt;
    public AnimationClip anim;
    private Animator animator;
    private BoxCollider2D col;
    public GameObject[] items;

    [SerializeField] bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        if (isOpened)
        {
            print("already open");
            return;
        }            
        animator.Play(anim.name);
        var spawnItem = items[Random.Range(0, items.Length)];
        Instantiate(spawnItem, transform.position, Quaternion.identity);
        isOpened = true;
        print("OPEN CHEST");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened)
        {
            print("already open");
            return;
        }
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

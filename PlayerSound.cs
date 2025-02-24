using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour, IObserver
{
    [SerializeField] AudioSource audioSource;

    [Header("Subjects")]    
    [SerializeField] Subject playerHealth;
    [SerializeField] Subject playerMovement;
    [SerializeField] Subject playerCombat;
    [SerializeField] Subject inventory;
    
    [Header("SFX")]
    [SerializeField] AudioClip[] pickSFX;
    [SerializeField] AudioClip[] jumpSFX;
    [SerializeField] AudioClip[] hurtSFX;
    [SerializeField] AudioClip[] attackSFX;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Pick):

                print("Play Pick Sound");
                audioSource.clip = pickSFX[Random.Range(0, pickSFX.Length)];
                audioSource.Play();
                return;

            case (PlayerAction.Attack):

                print("Play Attack Sound");
                audioSource.clip = jumpSFX[Random.Range(0, attackSFX.Length)];
                audioSource.Play();
                return;

            case (PlayerAction.Jump):
                audioSource.clip = jumpSFX[Random.Range(0, jumpSFX.Length)];
                audioSource.Play();
                return;

            case (PlayerAction.Hurt):

                print("Play Hurt Sound");
                audioSource.clip = hurtSFX[Random.Range(0,hurtSFX.Length)];
                audioSource.Play();
                return;
            
            default:
                return;
        }
    }

    void OnEnable()
    {
        playerHealth.AddObserver(this);
        playerMovement.AddObserver(this);
        playerCombat.AddObserver(this);
        inventory.AddObserver(this);
    }

    void OnDisable()
    {
        playerHealth.RemoveObserver(this);
        playerMovement.RemoveObserver(this);
        playerCombat.RemoveObserver(this);
        inventory.RemoveObserver(this);
    }
}

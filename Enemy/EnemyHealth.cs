using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [Header("Ref")]
    public Slider Slider;
    AudioSource audioSource;    
    public AudioClip gotSlashSound;

    [Header("Setting")]
    public bool isDead;
    [SerializeField] float maxHP = 100;    
    float currentHP;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // set initial hp
        currentHP = maxHP;
        ChangeHPBar(currentHP, maxHP);
    }

    private void Update()
    {
       ChangeHPBar(currentHP, maxHP);       
    }

    // update hp bar with the current value
    void ChangeHPBar(float currentValue, float maxValue)
    {
        Slider.value = currentValue / maxValue;
    }

    // used when the enemy is taking damage
    public void TakeDamage(float damage) 
    {
        if (isDead)
            return;

        currentHP -= damage;        
        if (currentHP <= 0)
        {            
            isDead = true;
        }

        audioSource.clip = gotSlashSound;
        audioSource.Play();
    }


}

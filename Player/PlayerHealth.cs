using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : Subject
{
    [Header("Ref")]
    public Slider Slider;
    public bool isDead;
    private AudioSource _audio;
    [SerializeField] AudioClip hurtSound;

    [Header("Setting")]
    [SerializeField] float maxHP;
    [SerializeField] float currentHP;
    void Start()
    {
        _audio = GetComponent<AudioSource>();

        // set initial health to max value
        currentHP = maxHP;
        ChangeHPBar(currentHP, maxHP);
    }

    private void Update()
    {
        // always update healthbar 
        ChangeHPBar(currentHP, maxHP);
    }

    // update health bar slider ui
    void ChangeHPBar(float currentValue, float maxValue)
    {
        Slider.value = currentValue / maxValue;
    }

    // This function will work when player take damage, be called from a damager
    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        //_audio.clip = hurtSound;
        //_audio.Play();
        currentHP -= damage;
        if (currentHP <= 0)
        {
            NotifyObserver(PlayerAction.Dead);
            isDead = true;
        }
        else
        {
            NotifyObserver(PlayerAction.Hurt);
        }        
    }

    // This function will work when player use a heal potion
    public void Heal(float amount)
    {
        if (currentHP + amount > maxHP) // prevent exeeded hp
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += amount;
        }
        print("HEAL UP");

    }
}

    

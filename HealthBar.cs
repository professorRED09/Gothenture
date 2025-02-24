using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public HealthBar instance;

	[SerializeField]
	Slider playerHealthBar;

	[SerializeField]
	private float maxHP;
	[SerializeField]
	private float currentHP;	

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		currentHP = maxHP;
	}

	public void Heal()
	{
		if (currentHP <= maxHP)
		{
			//currentHP += heal.abilValue;
			UpdateHealthBar(currentHP, maxHP);
		}

	}	

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			Debug.Log("OUCH");
			//TakeDamage(damage.abilValue);
		}
	}

	private void TakeDamage(float dmg)
	{
		currentHP -= dmg;
		UpdateHealthBar(currentHP, maxHP);

		//if (currentHP <= 0)
  //          NotifyObserver(Action.lose);
    }

	private void UpdateHealthBar(float currentValue, float maxValue)
	{
		playerHealthBar.value = currentValue / maxValue;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public float currHealth;

    public Image healthBar;
    public GameObject deathEffect;

    private void Start()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        healthBar.fillAmount= currHealth/maxHealth;
        if (currHealth <= 0)
        {
            Destroy(gameObject);
            BuildManager.cash += 2;
            GameObject effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(effect,3);
        }

    }


}

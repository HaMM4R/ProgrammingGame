using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject explosion; 

    void Start()
    {
    }
    
    void Update()
    {
        CheckHealth();
    }

    public void Damage(float damage)
    {
        playerHealth -= damage;
    }

    void CheckHealth()
    {
        if (playerHealth <= 0)
            Die(); 
    }

    void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
        
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 4 + 15, 38, 100,22), "Health: " + playerHealth.ToString());
    }
}

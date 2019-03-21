using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth; 

    void Start()
    {
        
    }
    
    void Update()
    {
        CheckHealth();
        if (Input.GetKeyDown(KeyCode.P))
            playerHealth -= 20; 
    }

    void CheckHealth()
    {
        if (playerHealth <= 0)
            Die(); 
    }

    void Die()
    {
        Destroy(gameObject);
    }
        
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 4 + 15, 38, 100,22), "Health: " + playerHealth.ToString());
    }
}
